using System.Collections.Generic;
using CodeBase.Cards;
using UnityEngine;

namespace CodeBase.Battle
{
    public class BattlePlayer : IPlayer
    {
        private string _id;
        public bool IsAlive => Health > 0;
        public string Id { get; }
        public int Health { get; set; }
        public int HandSize { get; set; }
        public List<ICard> Deck { get; private set; }
        public List<ICard> Hand { get; private set; }
        public List<ICard> SetToBattle { get; }
        public List<ICard> Discard { get; private set; }
        
        public void FillHandToFull()
        {
            while (Hand.Count < HandSize)
            {
                if (Deck.Count <= 0)
                {
                    foreach (var card in Discard) Hand.Add(card);
                    Discard.Clear();
                }
                
                var rndCard = Deck[Random.Range(0, Deck.Count)];
                Deck.Remove(rndCard);
                Hand.Add(rndCard);
            }
        }

        public void ClearHandsToDiscard()
        {
            foreach (var card in Hand)
                Discard.Add(card);
            
            Hand.Clear();
        }

        public void SetDamage(int damage)
        {
            Health -= damage;
        }

        public void SetLose()
        {
            
        }

        public void SetWin()
        {
           
        }

        public void DiscardCardsFromBattle()
        {
            foreach (var card in SetToBattle)
                Discard.Add(card);
            
            SetToBattle.Clear();
        }

        public BattlePlayer(List<ICard> deck, string id, int health, int handSize)
        {
            Id = id;
            Health = health;
            HandSize = handSize;
            Deck = new List<ICard>();
            foreach (var card in deck)
                Deck.Add(new BattleCard(card, id));
        }
    }
}