using CodeBase._Tools.StateMachine;
using UnityEngine;

namespace CodeBase.Battle
{
    public class PrepareHandsState : ISelfCompleteState
    {
        private IPlayersHolder _playersHolder;
        public bool Complete { get; private set; }
        private float _timer;
        
        public PrepareHandsState(IPlayersHolder playersHolder)
        {
            _playersHolder = playersHolder;
        }

        public void OnEnter()
        {
            _timer = 1.5f;
            Complete = false;
            foreach (IPlayer player in _playersHolder.BattlePlayers)
            {
                if (player.Hand.Count < player.HandSize)
                    player.FillHandToFull();
            }
        }

        public void OnExit()
        {
        }

        public void Tick()
        {
            _timer -= Time.deltaTime;
            Complete = _timer < 0;
        }


    }
}