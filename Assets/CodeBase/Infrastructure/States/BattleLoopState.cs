using CodeBase._Tools.StateMachine;
using CodeBase.Infrastructure.Factory;

namespace CodeBase.Infrastructure.States
{
    public class BattleLoopState : IState
    {
        public bool Win { get; private set; }
        public bool Loose { get; private set; }
        
        public BattleLoopState()
        {
            
        }
        
        public void OnEnter()
        {
            
        }

        public void OnExit()
        {

        }

        public void Tick()
        {
 
        }
    }
}