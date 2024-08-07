using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Cards
{
    public abstract class CardView : MonoBehaviour
    {
        [SerializeField] protected Image CardIcon;
        [SerializeField] protected TMP_Text Name;
        [SerializeField] protected TMP_Text Damage;
        [SerializeField] protected TMP_Text TypeName;
        
        protected ICard Card;

        public virtual void Init(ICard card)
        {
            Card = card;
            TypeName.text = card.Type.ToString();
            Name.text = card.Name;
            Damage.text = card.Damage.ToString();
        }
    }
}