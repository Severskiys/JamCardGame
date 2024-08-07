using TMPro;
using UnityEngine;

namespace CodeBase.UI.Mediators
{
    public class CardsHeapView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _cardsCountText;
        public void SetCardsCount(string text) => _cardsCountText.text = text;
    }
}