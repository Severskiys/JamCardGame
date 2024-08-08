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
                foreach (var player in _playersHolder.BattlePlayers)
                {
                    if (player.IsAlive)
                        player.SetWin();
                    else
                        player.SetLose();
                }
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