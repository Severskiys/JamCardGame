using System.Linq;
using CodeBase._Tools.StateMachine;
using UnityEngine;

namespace CodeBase.Battle
{
    public class CheckBattleEndState : IState
    {
        private readonly IPlayersHolder _playersHolder;
        private float _timer;
        
        public CheckBattleEndState(IPlayersHolder playersHolder)
        {
            _playersHolder = playersHolder;
        }
        
        public void OnEnter()
        {
            _timer = 1.55f;
        }

        public void OnExit()
        {
         
        }

        public void Tick()
        {
            _timer -= Time.deltaTime;

            if (_timer < 0)
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
    }
}