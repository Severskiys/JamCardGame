using System.Collections.Generic;
using CodeBase.Cards;

namespace CodeBase.Battle
{
    public interface IPlayer
    {
        public bool IsAlive { get; }
        public string Id { get;}
        public int Health { get;  }
        public int HandSize { get; }
        public List<ICard> Deck { get; }
        public List<ICard> Hand { get; }
        public List<ICard> SetToBattle { get; }
        public List<ICard> Discard { get;  }
        public void FillHandToFull();
        public void ClearHandsToDiscard();
        public void SetDamage(int damage);
        public void SetLose();
        public void SetWin();
        public void DiscardCardsFromBattle();
    }
}