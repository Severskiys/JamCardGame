using System.Collections.Generic;
using CodeBase.Cards;

namespace CodeBase.Battle
{
    public interface IPlayer
    {
        public string Id { get; set; }
        public int Health { get; set; }
        public int HandSize { get; set; }
        public List<ICard> Deck { get; }
        public List<ICard> Hand { get; }
        public List<ICard> SetToBattle { get; }
        public List<ICard> Discard { get;  }
        public void FillHandToFull();
        public void ClearHandsToDiscard();
    }
}