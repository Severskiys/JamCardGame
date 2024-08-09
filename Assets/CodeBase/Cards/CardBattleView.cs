using CodeBase.UI;
using CodeBase.UI.Mediators;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CodeBase.Cards
{
    public class CardBattleView : CardView, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private ICard _battleCard;

        [SerializeField] private Image _back;
        [SerializeField] private Image _bannedImage;
        [SerializeField] private Image _front;
        
        public override void Init(ICard card)
        {
            base.Init(card);
            _battleCard = card;
            _battleCard.OnShowEqual += ShowEqual;
            _battleCard.OnShowWin += ShowWin;
            _battleCard.OnShowLose += ShowLose;
            _battleCard.OnChangeState += SetBannedView;
            SetBannedView();
        }

        public void SetBannedView() => _bannedImage.gameObject.SetActive(Card.IsBanned);
        private void ShowEqual() => transform.DOScale(1.25f, 0.2f).OnComplete(() => transform.DOScale(1.0f, 0.15f));
        private void ShowWin() => transform.DOScale(1.15f, 0.2f).OnComplete(() => transform.DOScale(1.35f, 0.15f));
        private void ShowLose() => transform.DOScale(1.1f, 0.2f).OnComplete(() => transform.DOScale(0.65f, 0.15f));

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (Card.IsBanned)
                return;
            _front.raycastTarget = false;
            transform.localScale = Vector3.one * 1.125f;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (Card.IsBanned)
                return;
            
            transform.localPosition += new Vector3(eventData.delta.x, eventData.delta.y, 0) * UiRoot.GetScale();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (Card.IsBanned)
                return;
            _front.raycastTarget = true;
            transform.localScale = Vector3.one;
            
            if (eventData != null
                && eventData.pointerEnter.TryGetComponent(out BattleSlotView battleSlot)
                && _battleCard.TrySetInBattleSlot(battleSlot.Index))
            {
                transform.SetParent(battleSlot.transform);
                transform.localPosition = Vector3.zero;// battleSlot.transform.position;
            }
            else
            {
                ReturnCard();
            }
        }

        public void Show()
        {
            _back.DOColor(Color.clear, 0.25f);
            transform.DORotateQuaternion(Quaternion.Euler(0, 180, 0), 0).SetEase(Ease.Linear)
                .OnComplete(() => transform.DORotateQuaternion(Quaternion.Euler(0, 0, 0), 0.2f).SetEase(Ease.Linear));
        }

        public void Discard()
        {
            _battleCard.OnShowEqual -= ShowEqual;
            _battleCard.OnShowWin -= ShowWin;
            _battleCard.OnShowLose -= ShowLose;
            _battleCard.OnChangeState -= SetBannedView;
        }

        private void ReturnCard()
        {
            transform.localPosition = Vector3.zero;
        }
    }
}