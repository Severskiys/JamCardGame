using CodeBase.UI.Mediators;
using UnityEngine.EventSystems;

namespace CodeBase.Cards
{
    public class CardBattleView : CardView, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private BattleCard _battleCard;

        public void Init(BattleCard battleCard)
        {
            _battleCard = battleCard;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            /*
             
             _itemConfig.DisableNotification();
              SetNotification();
              
              _draggedItemView.transform.position = transform.position;
              _draggedItemView.SetSprite(_itemConfig.Icon);
              _itemView.color = Color.clear;
              _draggedItemView.SetColor(Color.white);
              
              OnBeginDragItem?.Invoke(_itemConfig);
              
              */
        }

        public void OnDrag(PointerEventData eventData)
        {
            //  _draggedItemView.Move(eventData.delta);
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
        }
    }
}