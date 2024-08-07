using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CodeBase.Cards
{
    public class CardSelectorView : CardView, IPointerDownHandler, IPointerUpHandler
    {
        public event Action<ICard> OnClick;
        
        [SerializeField] private Image _isSelectedIcon;
        
        public override void Init(ICard card)
        {
            base.Init(card);
            card.OnChangeState += SetSelectionView;
            SetSelectionView();
        }

        private void OnDisable() => Card.OnChangeState -= SetSelectionView;

        private void SetSelectionView() => _isSelectedIcon.gameObject.SetActive(Card.IsSelected);

        public void OnPointerUp(PointerEventData eventData) => OnClick?.Invoke(Card);

        public void OnPointerDown(PointerEventData eventData)
        {
            
        }
    }
}