using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CodeBase.Cards
{
    public class CardBattleView : CardView, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
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
          /*
           if (eventData != null && eventData.pointerEnter.TryGetComponent(out BattleSlot battleSlot))
            {
                battleSlot.SetCardView(this);
            }
            else
            {
                ReturnCard();
            }
            */
        }
        
        private void ReturnCard()
        {
           
        }
    }
}