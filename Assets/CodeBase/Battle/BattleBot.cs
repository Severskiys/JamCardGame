using System.Collections.Generic;
using CodeBase.Cards;

namespace CodeBase.Battle
{
    public class BattleBot : BattlePlayer
    {
        public BattleBot(List<ICard> deck, string id, int health, int handSize, IBattleRoom room) : base(deck, id, health, handSize, room)
        {
        }
    }
}