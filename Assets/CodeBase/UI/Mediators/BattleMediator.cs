using System;
using System.Collections.Generic;
using CodeBase._Tools.Helpers;
using CodeBase.Cards;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Mediators
{
    public class BattleMediator : MonoBehaviour, IMediator
    {
        public event Action<IMediator> OnCleanUp;
        
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private List<BattleSlotView> _playerSlots;
        [SerializeField] private List<BattleSlotView> _enemySlots;
        [SerializeField] private CardsHeapView _deckView;
        [SerializeField] private CardsHeapView _discardView;
        [SerializeField] private Image _playerHpView;
        [SerializeField] private Image _enemyHpView;
        [SerializeField] private List<HandSlot> _handSlots;

        private List<CardBattleView> _spawnedCards = new();
        public GameObject GameObject => gameObject;
        public void Show() => _canvasGroup.Show();
        public void Hide() => _canvasGroup.Hide();
        private void OnDestroy() => OnCleanUp?.Invoke(this);

        public void SetDeckStats(int deckCount, int discardCount)
        {
            _deckView.SetCardsCount(deckCount.ToString());
            _discardView.SetCardsCount(discardCount.ToString());
        }

        public void SetHand(List<ICard> playerHand)
        {
            for (var i = 0; i < playerHand.Count; i++)
            {
                //спавним карты
            }
        }
    }
}