using System.Collections.Generic;
using System.Linq;
using CodeBase._Tools.StateMachine;

namespace CodeBase.Battle
{
    public class WaitPlayerInputState : ISelfCompleteState
    {
        private List<BattleSlot> _slots;
        private IPlayersHolder _playersHolder;
        public bool Complete { get; private set; }
        
        public WaitPlayerInputState(List<BattleSlot> slots, IPlayersHolder playersHolder)
        {
            _playersHolder = playersHolder;
            _slots = slots;
        }

        public void OnEnter()
        {
            Complete = false;
        }

        public void OnExit()
        {
            foreach (var player in _playersHolder.BattlePlayers)
                player.SetPrepareToCompareState();
        }

        public void Tick()
        {
            Complete = _slots.All(s=>s.FullyEquipped);
        }
    }
}