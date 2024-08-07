using System.Collections.Generic;
using System.Linq;
using CodeBase._Tools.StateMachine;
using CodeBase.Cards;
using UnityEngine;

namespace CodeBase.Battle
{
    public class ResolveCardsInteractionState : ISelfCompleteState
    {
        private IPlayersHolder _playersHolder;
        private List<BattleSlot> _slots;
        private CardArbiterService _cardArbiterService;
        private float _timer;
        
        public bool Complete { get; private set; }
        
        public ResolveCardsInteractionState(IPlayersHolder playersHolder, List<BattleSlot> slots, CardArbiterService cardArbiterService)
        {
            _cardArbiterService = cardArbiterService;
            _slots = slots;
            _playersHolder = playersHolder;
        }
        
        public void OnEnter()
        {
            Complete = false;
            _timer = 5.0f;
            foreach (var battleSlot in _slots)
            {
                (bool hasWinner, string winnerId) result = _cardArbiterService.DetermineWinner(battleSlot.CardsInSlot);

                foreach (var card in battleSlot.CardsInSlot)
                {
                    ApplyCardsEffects(card);
                }
                
                if (result.hasWinner)
                {
                    ICard winCard = battleSlot.CardsInSlot.FirstOrDefault(c => c.PlayerId == result.winnerId);
                    ICard loseCard = battleSlot.CardsInSlot.FirstOrDefault(c => c.PlayerId != result.winnerId);
                    IPlayer loser = _playersHolder.BattlePlayers.FirstOrDefault(p => p.Id != result.winnerId);
                    
                    winCard!.SetWin();
                    loseCard!.SetLose();
                    loser!.SetDamage(winCard.Damage);
                }
                else
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

        private void ApplyCardsEffects(ICard card)
        {
            
        }

        public void OnExit()
        {
            foreach (var player in _playersHolder.BattlePlayers)
                player.DiscardCardsFromBattle();
        }

        public void Tick()
        {
            _timer -= Time.deltaTime;
            Complete = _timer < 0;
        }
    }
}