using System;
using System.Collections.Generic;
using CodeBase._Tools.Helpers;
using CodeBase.Cards;
using DG.Tweening;
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
        [SerializeField] private CardBattleView _cardPrefab;

        private readonly Dictionary<ICard, CardBattleView> _spawnedCards = new();
        public GameObject GameObject => gameObject;
        public void Show() => _canvasGroup.Show();
        public void Hide() => _canvasGroup.Hide();
        private void OnDestroy() => OnCleanUp?.Invoke(this);

        public void Init(int deckCount, int discardCount)
        {
            _deckView.SetCardsCount(deckCount.ToString());
            _discardView.SetCardsCount(discardCount.ToString());
            
            for (int i = 0; i < _playerSlots.Count; i++)
                _playerSlots[i].Init(i);
            
            for (int i = 0; i < _enemySlots.Count; i++)
                _enemySlots[i].Init(i);
        }

        public void SetHand(List<ICard> playerHand)
        {
            ClearSpawnedCards();

            for (var i = 0; i < playerHand.Count; i++)
            {
                CardBattleView instance = Instantiate(_cardPrefab, _deckView.transform);
                instance.transform.SetParent(_handSlots[i].transform, true);
                instance.transform.DOLocalJump(Vector3.zero, 1.0f, 1, 0.25f).SetLink(instance.gameObject);
                _spawnedCards.Add(playerHand[i], instance);
                instance.Init(playerHand[i]);
            }
        }

        private void ClearSpawnedCards()
        {
            foreach (var card in _spawnedCards)
            {
                card.Value.Discard();
                Destroy(card.Value.gameObject);
            }
            _spawnedCards.Clear();
        }

        public void SetEnemyHealth(int health, int maxHealth) => _enemyHpView.fillAmount = (float)health / maxHealth;
        public void SetPlayerHealth(int health, int maxHealth) => _playerHpView.fillAmount = (float)health / maxHealth;

        public void SetEnemyBattleCards(List<ICard> cards)
        {
            for (var i = 0; i < cards.Count; i++)
            {
                CardBattleView instance = Instantiate(_cardPrefab, _enemySlots[i].transform);
                instance.transform.DOScale(0, 0).SetLink(instance.gameObject);
                instance.transform.DOScale(1.0f, 0.75f).SetLink(instance.gameObject);
                _spawnedCards.Add(cards[i], instance);
                instance.Init(cards[i]);
            }
        }

        public void MoveCardsToDiscard(List<ICard> cards)
        {
            foreach (var card in cards)
            {
                _spawnedCards[card].transform.DOMove(_discardView.transform.position, 0.35f).SetLink(_spawnedCards[card].gameObject);
                _spawnedCards[card].transform.DOScale(.25f, 0.35f).SetLink(_spawnedCards[card].gameObject);
            }
        }
    }
}