using System;
using CodeBase._Tools.Helpers;
using CodeBase.Infrastructure;
using UnityEngine;
using VContainer;

namespace CodeBase.UI.Mediators
{
    public class MenuMediator : MonoBehaviour, IMediator
    {
        public event Action<IMediator> OnCleanUp;

        [SerializeField] private SimpleButton _startGameButton;
        [SerializeField] private SimpleButton _openCardSelector;
        [SerializeField] private CanvasGroup _canvasGroup;
        
        private IGameplayStarter _gameplayStarter;
        private MediatorFactory _mediatorFactory;

        public GameObject GameObject => gameObject;
        public void Show() => _canvasGroup.Show();
        public void Hide() => _canvasGroup.Hide();

        [Inject]
        public void Construct(IGameplayStarter gameplayStarter, MediatorFactory mediatorFactory)
        {
            _mediatorFactory = mediatorFactory;
            _gameplayStarter = gameplayStarter;
        }
        
        private void Awake()
        {
            _startGameButton.OnClick += SendStartGameSignal;
            _openCardSelector.OnClick += OpenCardsSelector;
        }

        private void OnDestroy()
        {
            _startGameButton.OnClick -= SendStartGameSignal;
            _openCardSelector.OnClick -= OpenCardsSelector;
            OnCleanUp?.Invoke(this);
        }

        private async void OpenCardsSelector()
        {
            await _mediatorFactory.Show<CardSelectorMediator>();
        }

        private void SendStartGameSignal() => _gameplayStarter.LoadGameLevel();
    }
}