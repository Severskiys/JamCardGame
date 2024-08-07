using System.Collections.Generic;
using CodeBase.Cards;

namespace CodeBase.Battle
{
    public interface IBattleRoom
    {
        public BattlePlayer CreatePlayer();
        public BattlePlayer CreateBot();
        public void StartBattle();
        public bool TrySetCard(ICard cardId, int slotIndex);
        public void Tick();
    }
}