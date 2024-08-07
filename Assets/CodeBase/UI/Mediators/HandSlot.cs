using UnityEngine;

namespace CodeBase.UI.Mediators
{
    public class HandSlot : MonoBehaviour
    {
        private bool _occupied;
        public bool Occupied => _occupied;
        
        public void SetCard()
        {
            _occupied = true;
            gameObject.SetActive(_occupied);
        }

        public void RemoveCard()
        {
            _occupied = false;
            gameObject.SetActive(_occupied);
        }
    }
}