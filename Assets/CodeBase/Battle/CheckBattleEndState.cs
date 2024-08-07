using CodeBase._Tools.StateMachine;

namespace CodeBase.Battle
{
    public class CheckBattleEndState : IState
    {
        public void OnEnter()
        {
            ContinueBattle = false;
            EndBattle = false;
        }

        public void OnExit()
        {
         
        }

        public void Tick()
        {
        
        }

        public bool ContinueBattle { get; private set; }
        public bool EndBattle { get; private set; }
    }
}