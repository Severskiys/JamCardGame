using CodeBase.UI.Mediators;
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
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            /*
                           
              _draggedItemView.transform.position = transform.position;
              _draggedItemView.SetSprite(_itemConfig.Icon);
              _itemView.color = Color.clear;
              _draggedItemView.SetColor(Color.white);
              OnBeginDragItem?.Invoke(_itemConfig);
            */
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.localPosition += (Vector3)eventData.delta;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
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

        private void ReturnCard()
        {
            transform.localPosition = Vector3.zero;
        }
    }
}