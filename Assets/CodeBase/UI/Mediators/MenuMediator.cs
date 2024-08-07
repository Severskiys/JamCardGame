using System;
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
        
        private IGameplayStarter _gameplayStarter;
        private CardSelectorMediator _selectorMediator;
        public GameObject GameObject => gameObject;

        [Inject]
        public async void Construct(IGameplayStarter gameplayStarter, MediatorFactory mediatorFactory)
        {
            _gameplayStarter = gameplayStarter;
            _selectorMediator = await mediatorFactory.Get<CardSelectorMediator>();
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

        private void OpenCardsSelector() => _selectorMediator.OpenWindow();
        private void SendStartGameSignal() => _gameplayStarter.LoadGameLevel();
    }
}