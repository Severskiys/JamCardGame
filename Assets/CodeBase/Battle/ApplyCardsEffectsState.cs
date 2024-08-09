using System.Collections.Generic;
using System.Linq;
using CodeBase._Tools.StateMachine;
using CodeBase.Cards;
using UnityEngine;

namespace CodeBase.Battle
{
    public class ApplyCardsEffectsState : ISelfCompleteState
    {
        private readonly IPlayersHolder _playersHolder;
        private readonly List<BattleSlot> _slots;
        private float _timer;

        public bool Complete { get; private set; }

        public ApplyCardsEffectsState(IPlayersHolder playersHolder, List<BattleSlot> slots)
        {
            _slots = slots;
            _playersHolder = playersHolder;
        }

        public void OnEnter()
        {
            Complete = false;
            _timer = 0.5f;
            foreach (var battleSlot in _slots)
                battleSlot.CardsInSlot.ForEach(ApplyCardsEffects);
        }

        public void OnExit()
        {
        }

        public void Tick()
        {
            _timer -= Time.deltaTime;
            Complete = _timer < 0;
        }

        private void ApplyCardsEffects(ICard card)
        {
            var cardOwner = _playersHolder.BattlePlayers.FirstOrDefault(p => p.Id == card.PlayerId);
            var enemy = _playersHolder.BattlePlayers.FirstOrDefault(p => p.Id != card.PlayerId);

            switch (card.EffectType)
            {
                case EffectType.Heal:
                    cardOwner!.Heal(card.GetEffectStat(StatType.Amount));
                    break;
                case EffectType.Bleed:
                    enemy!.AddEffect(new Effect(card.EffectType, card.EffectStats));
                    break;
                case EffectType.Shield:
                    cardOwner!.AddShield(card.GetEffectStat(StatType.Amount));
                    break;
                case EffectType.Dispel:
                    cardOwner!.ClearAllEffects();
                    break;
                case EffectType.PaperBan:
                    enemy!.AddEffect(new Effect(card.EffectType, card.EffectStats));
                    break;
                case EffectType.ScissorsBan:
                    enemy!.AddEffect(new Effect(card.EffectType, card.EffectStats));
                    break;
                case EffectType.StoneBan:
                    enemy!.AddEffect(new Effect(card.EffectType, card.EffectStats));
                    break;
                case EffectType.HealBan:
                    enemy!.AddEffect(new Effect(card.EffectType, card.EffectStats));
                    break;
                case EffectType.HealOnCardWin:
                    cardOwner!.AddEffect(new Effect(card.EffectType, card.EffectStats));
                    break;
                case EffectType.ShieldOnLoose:
                    cardOwner!.AddEffect(new Effect(card.EffectType, card.EffectStats));
                    break;
                case EffectType.AttackBonus:
                    cardOwner!.AddEffect(new Effect(card.EffectType, card.EffectStats));
                    break;
            }
        }
    }
}