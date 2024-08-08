using CodeBase.UI.Mediators;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.Cards
{
    public class CardBattleView : CardView, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private ICard _battleCard;

        public override void Init(ICard card)
        {
            base.Init(card);
            _battleCard = card;
            _battleCard.OnShowEqual += ShowEqual;
            _battleCard.OnShowWin += ShowWin;
            _battleCard.OnShowLose += ShowLose;
        }

        private void ShowEqual() => transform.DOScale(1.25f, 0.2f).OnComplete(() => transform.DOScale(1.0f, 0.15f));
        private void ShowWin() => transform.DOScale(1.15f, 0.2f).OnComplete(() => transform.DOScale(1.35f, 0.15f));
        private void ShowLose() => transform.DOScale(1.1f, 0.2f).OnComplete(() => transform.DOScale(0.65f, 0.15f));

        public void OnBeginDrag(PointerEventData eventData)
        {
            transform.localScale = Vector3.one * 1.125f;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.localPosition += (Vector3)eventData.delta;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.localScale = Vector3.one;
            
            if (eventData != null
                && eventData.pointerEnter.TryGetComponent(out BattleSlotView battleSlot)
                && _battleCard.TrySetInBattleSlot(battleSlot.Index))
            {
                transform.position = battleSlot.transform.position;
            }
            else
            {
                ReturnCard();
            }
        }

        public void Discard()
        {
            _battleCard.OnShowEqual -= ShowEqual;
            _battleCard.OnShowWin -= ShowWin;
            _battleCard.OnShowLose -= ShowLose;
        }

        private void ReturnCard()
        {
            transform.localPosition = Vector3.zero;
        }
    }
}