using CodeBase._Tools.StateMachine;
using CodeBase.Cards;
using UnityEngine;

namespace CodeBase.Battle
{
    public class ProcessEndOfTurnEffects : ISelfCompleteState
    {
        private IPlayersHolder _playersHolder;
        private float _timer;
        public bool Complete { get; private set; }

        public ProcessEndOfTurnEffects(IPlayersHolder playersHolder)
        {
            _playersHolder = playersHolder;
        }
        
        public void OnEnter()
        {
            _timer = 0.5f;
            Complete = false;
            foreach (var player in _playersHolder.BattlePlayers)
            {
                if (player.HaveEffect(EffectType.Bleed))
                    player.SetDamage(Mathf.CeilToInt(player.GetEffectValue(EffectType.Bleed)));
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