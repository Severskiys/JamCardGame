using CodeBase._Tools.StateMachine;

namespace CodeBase.Battle
{
    public class ProcessEndOfTurnEffects : ISelfCompleteState
    {
        private IPlayersHolder _playersHolder;
        public bool Complete { get; private set; }

        public ProcessEndOfTurnEffects(IPlayersHolder playersHolder)
        {
            _playersHolder = playersHolder;
        }
        
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