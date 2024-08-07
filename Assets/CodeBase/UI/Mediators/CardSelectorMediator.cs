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
        private MediatorFactory _mediatorFactory;
        public GameObject GameObject => gameObject;
        public void Show() => _canvasGroup.Show();
        public void Hide() => _canvasGroup.Hide();

        [Inject]
        public void Construct(CardsService cardsService, MediatorFactory mediatorFactory)
        {
            _mediatorFactory = mediatorFactory;
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
            Hide();
            _closeSelectorWindow.OnClick += ProcessCloseWindow;
        }

        private void OnDestroy()
        {
            _closeSelectorWindow.OnClick -= ProcessCloseWindow;
            _spawnedCards.ForEach(sc=>sc.OnClick -= ProcessCardClick);
            OnCleanUp?.Invoke(this);
        }

        private async void ProcessCloseWindow() => await _mediatorFactory.Show<MenuMediator>();
    }
}