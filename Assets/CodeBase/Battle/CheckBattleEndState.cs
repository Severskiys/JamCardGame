using System.Linq;
using CodeBase._Tools.StateMachine;

namespace CodeBase.Battle
{
    public class CheckBattleEndState : IState
    {
        private IPlayersHolder _playersHolder;

        public CheckBattleEndState(IPlayersHolder playersHolder)
        {
            _playersHolder = playersHolder;
        }
        
        public void OnEnter()
        {
            ContinueBattle = _playersHolder.BattlePlayers.All(p=> p.IsAlive);

            if (ContinueBattle == false)
            {
                var loser = _playersHolder.BattlePlayers.FirstOrDefault(p => p.IsAlive == false);
                var winner = _playersHolder.BattlePlayers.FirstOrDefault(p => p.IsAlive);

                loser!.SetLose();
                if (winner != default)
                    winner.SetWin();
                
            }
        }

        public void OnExit()
        {
         
        }

        public void Tick()
        {
        
        }

        public bool ContinueBattle { get; private set; }
       
    }
}