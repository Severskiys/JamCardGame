using System;
using System.Collections.Generic;
using CodeBase.StaticData;
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
        [SerializeField] protected List<Image> TypeName;
        [SerializeField] protected Sprite Skissors;
        [SerializeField] protected Sprite Paper;
        [SerializeField] protected Sprite Rock;
        
        [SerializeField] protected Sprite Skissors_big;
        [SerializeField] protected Sprite Paper_big;
        [SerializeField] protected Sprite Rock_big;
        protected ICard Card;

        public virtual void Init(ICard card)
        {
            Card = card;
            TypeName.ForEach(img=> img.sprite = card.Type switch
            {
                CardType.Rock => Rock,
                CardType.Scissor => Skissors,
                CardType.Paper => Paper,
                _ => Paper
            }); 
            Name.text = card.Name;
            Damage.text = card.Damage.ToString();
            CardIcon.sprite = card.Type switch
            {
                CardType.Rock => Rock_big,
                CardType.Scissor => Skissors_big,
                CardType.Paper => Paper_big,
                _ => Paper
            };
        }
    }
}