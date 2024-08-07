using System.Collections.Generic;
using System.Linq;
using CodeBase._Tools.StateMachine;

namespace CodeBase.Battle
{
    public class WaitPlayerInputState : ISelfCompleteState
    {
        private List<BattleSlot> _slots;
        public bool Complete { get; private set; }
        
        public WaitPlayerInputState(List<BattleSlot> slots)
        {
            _slots = slots;
        }

        public void OnEnter()
        {
            Complete = false;
        }

        public void OnExit()
        {
        }

        public void Tick()
        {
            Complete = _slots.All(s=>s.FullyEquipped);
        }
    }
}