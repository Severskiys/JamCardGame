
using System.Collections.Generic;
using System.Linq;
using CodeBase.Cards;
using UnityEngine;

namespace CodeBase.Battle
{
    public class BattleBot : BattlePlayer
    {
        private readonly List<BattleSlot> _slots;

        public BattleBot(List<ICard> deck, string id, int health, int handSize, IBattleRoom room, List<BattleSlot> slots) : base(deck, id, health, handSize, room)
        {
            _slots = slots;
        }

        public override void StartSelectingCardsToBattle()
        {
            var availableCards = Hand.Where(c => c.IsBanned == false).ToList();
            for (var index = 0; index < _slots.Count; index++)
            {
                var rndCard = availableCards[Random.Range(0, availableCards.Count)];
                TrySetCard(rndCard, index);
            }
            
            OnSetBattleCards?.Invoke();
            base.StartSelectingCardsToBattle();
        }
    }
}