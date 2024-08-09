using System.Collections.Generic;
using System.Linq;
using CodeBase._Tools.StateMachine;
using CodeBase.Cards;
using UnityEngine;

namespace CodeBase.Battle
{
    public class ResolveCardsInteractionState : ISelfCompleteState
    {
        private readonly IPlayersHolder _playersHolder;
        private readonly List<BattleSlot> _slots;
        private readonly CardArbiterService _cardArbiterService;
        private float _timer;

        public int EnterStateCount { get; private set; }
        public bool Complete { get; private set; }

        public ResolveCardsInteractionState(IPlayersHolder playersHolder, List<BattleSlot> slots,
            CardArbiterService cardArbiterService)
        {
            _cardArbiterService = cardArbiterService;
            _slots = slots;
            _playersHolder = playersHolder;
        }

        public void OnEnter()
        {
            EnterStateCount += 1;
            Complete = false;
            _timer = 1.5f;
            foreach (var battleSlot in _slots)
            {
                (bool hasWinner, string winnerId) result = _cardArbiterService.DetermineWinner(battleSlot.CardsInSlot);
                if (result.hasWinner) ProcessWin(battleSlot, result);
                else ProcessDraw(battleSlot);
            }
        }

        public void OnExit()
        {
            if (_playersHolder.BattlePlayers.All(p => p.IsAlive))
            {
                foreach (var player in _playersHolder.BattlePlayers)
                    player.DiscardCardsFromBattle();

                foreach (var slot in _slots)
                    slot.ClearSlot();
            }
        }

        public void Tick()
        {
            _timer -= Time.deltaTime;
            Complete = _timer < 0;
        }
        
        private void ProcessWin(BattleSlot battleSlot, (bool hasWinner, string winnerId) result)
        {
            ICard winCard = battleSlot.CardsInSlot.FirstOrDefault(c => c.PlayerId == result.winnerId);
            ICard loseCard = battleSlot.CardsInSlot.FirstOrDefault(c => c.PlayerId != result.winnerId);
            IPlayer loser = _playersHolder.BattlePlayers.FirstOrDefault(p => p.Id != result.winnerId);
            IPlayer winner = _playersHolder.BattlePlayers.FirstOrDefault(p => p.Id == result.winnerId);
            winCard!.SetWin();
            loseCard!.SetLose();
            int damage = winCard.Damage;
            if (winner!.HaveEffect(EffectType.AttackBonus)) damage += (int)winner.GetEffectValue(EffectType.AttackBonus);
            if (winner!.HaveEffect(EffectType.HealOnCardWin)) winner.Heal(winner.GetEffectValue(EffectType.HealOnCardWin));
            if (loser!.HaveEffect(EffectType.ShieldOnLoose)) loser.AddShield(loser.GetEffectValue(EffectType.ShieldOnLoose));
            loser!.SetDamage(damage);
            loser!.SetDamage(damage);
        }

        private void ProcessDraw(BattleSlot battleSlot)
        {
            ICard firstCard = battleSlot.CardsInSlot[0];
            ICard secondCard = battleSlot.CardsInSlot[1];
            IPlayer firstPlayer = _playersHolder.BattlePlayers.FirstOrDefault(p => p.Id == firstCard.PlayerId);
            IPlayer secondPlayer = _playersHolder.BattlePlayers.FirstOrDefault(p => p.Id == secondCard.PlayerId);
            firstCard.SetEqual();
            secondCard.SetEqual();
            firstPlayer!.SetDamage(Mathf.CeilToInt(secondCard.Damage * 0.5f));
            secondPlayer!.SetDamage(Mathf.CeilToInt(firstCard.Damage * 0.5f));
        }
    }
}