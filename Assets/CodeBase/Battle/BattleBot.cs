
using System.Collections.Generic;
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
            for (var index = 0; index < _slots.Count; index++)
            {
                var rndCard = Hand[Random.Range(0, Hand.Count)];
                TrySetCard(rndCard, index);
            }
            
            OnSetBattleCards?.Invoke();
            base.StartSelectingCardsToBattle();
        }
    }
}