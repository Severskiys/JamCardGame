using System.Collections.Generic;
using CodeBase.Cards;

namespace CodeBase.Battle
{
    public interface IBattleRoom
    {
        public string Connect(List<ICard> cards);
        public bool TrySetCard(ICard cardId, int slotIndex);
        public void Tick();
    }
}