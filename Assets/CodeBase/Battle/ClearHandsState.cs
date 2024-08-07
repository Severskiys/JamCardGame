using CodeBase._Tools.StateMachine;
using UnityEngine;

namespace CodeBase.Battle
{
    public class ClearHandsState : ISelfCompleteState
    {
        private readonly IPlayersHolder _playersHolder;
        private float _timer;
        
        public ClearHandsState(IPlayersHolder playersHolder)
        {
            _playersHolder = playersHolder;
        }
        
        public void OnEnter()
        {
            Complete = false;
            _playersHolder.BattlePlayers.ForEach(bp=>bp.ClearHandsToDiscard());
            _timer = 1.5f;
        }

        public void OnExit()
        {
            
        }

        public void Tick()
        {
            _timer -= Time.deltaTime;
            Complete = _timer < 0;
        }

        public bool Complete { get; private set; }
    }
}