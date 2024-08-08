using CodeBase._Tools.StateMachine;

namespace CodeBase.Battle
{
    public class ProcessEndOfTurnEffects : ISelfCompleteState
    {
        public bool Complete { get; private set; }

        public void OnEnter()
        {
            Complete = true;
        }

        public void OnExit()
        {
         
        }

        public void Tick()
        {

        }
    }
}