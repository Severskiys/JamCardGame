using System;
using System.Collections.Generic;
using CodeBase._Tools.Helpers;
using CodeBase.Cards;
using TMPro;
using UnityEngine;
using VContainer;

namespace CodeBase.UI.Mediators
{
    public class CardSelectorMediator : MonoBehaviour, IMediator
    {
        public event Action<IMediator> OnCleanUp;

        [SerializeField] private SimpleButton _closeSelectorWindow;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Transform _cardsParent;
        [SerializeField] private CardSelectorView _cardPrefab;
        [SerializeField] private TMP_Text _cardsCount;

        private readonly List<CardSelectorView> _spawnedCards = new List<CardSelectorView>();
        private CardsService _cardsService;
        public GameObject GameObject => gameObject;

        [Inject]
        public void Construct(CardsService cardsService)
        {
            _cardsService = cardsService;
            foreach (var card in _cardsService.AllCards)
            {
                CardSelectorView cardView = Instantiate(_cardPrefab, _cardsParent);
                cardView.Init(card);
                _spawnedCards.Add(cardView);
                cardView.OnClick += ProcessCardClick;
            }
            SetCardsSelectedCount();
        }

        private void ProcessCardClick(ICard clickedCard)
        {
            _cardsService.TryChangeCardSelection(clickedCard);
            SetCardsSelectedCount();
        }

        private void SetCardsSelectedCount() 
            => _cardsCount.text = _cardsService.SelectedCardsCount + " / " + _cardsService.DeckSize;

        public void OpenWindow() => _canvasGroup.Show(0.15f);

        private void Awake()
        {
            CloseWindow();
            _closeSelectorWindow.OnClick += CloseWindow;
        }

        private void OnDestroy()
        {
            _closeSelectorWindow.OnClick -= CloseWindow;
            _spawnedCards.ForEach(sc=>sc.OnClick -= ProcessCardClick);
            OnCleanUp?.Invoke(this);
        }

        private void CloseWindow() => _canvasGroup.Hide();
    }
}