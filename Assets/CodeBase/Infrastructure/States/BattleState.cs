using System;
using CodeBase._Services.StaticData;
using CodeBase._Tools.StateMachine;
using CodeBase.Battle;
using CodeBase.Cards;
using CodeBase.Logic;
using CodeBase.UI.Mediators;

namespace CodeBase.Infrastructure.States
{
    public class BattleState : IState
    {
        public bool Win { get; private set; }
        public bool Loose { get; private set; }
        
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly StaticDataService _staticData;
        private readonly MediatorFactory _mediatorFactory;
        private readonly CardsService _cardsService;
        private readonly CardArbiterService _cardArbiterService;
        private IBattleRoom _room;
        private BattleMediator _battleMediator;
        private BattlePlayer _player;
        private BattlePlayer _bot;
        private bool _isLoaded;

        public BattleState(SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            StaticDataService staticDataService,
            MediatorFactory mediatorFactory, CardsService cardsService, CardArbiterService cardArbiterService)
        {
            _cardArbiterService = cardArbiterService;
            _cardsService = cardsService;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _staticData = staticDataService;
            _mediatorFactory = mediatorFactory;
        }

        public async void OnEnter()
        {
            _isLoaded = false;
            _battleMediator = await _mediatorFactory.Get<BattleMediator>();
            _room = new BotBattleRoom(_cardsService, _staticData, _cardArbiterService);
            _player = _room.CreatePlayer();
            _bot = _room.CreateBot();
            _player.OnChangeHealth += SetPlayerHealth;
            _player.OnMoveHandToDiscard += MoveHandCardsToDiscard;
            _player.OnMoveCardsFromBattleToDiscard += MovePlayerBattleCardsToDiscard;
            _bot.OnMoveCardsFromBattleToDiscard += MoveEnemyBattleCardsToDiscard;
            _bot.OnChangeHealth += SetEnemyHealth;
            _bot.OnSetBattleCards += SetEnemyBattleCards;
            _player.OnFillHand += ProcessHandFill;
            _player.OnWin += ShowWin;
            _player.OnLose += ShowLoose;
            _room.StartBattle();
            InitBattleMediator();
            await _mediatorFactory.Show<BattleMediator>();
            _isLoaded = true;
        }
        
        private void ProcessHandFill()
        {
            _battleMediator.SetHand(_player.Hand);
            InitBattleMediator();
        }

        public void OnExit()
        {
            _room.Dispose();
            _player.OnChangeHealth -= SetPlayerHealth;
            _bot.OnChangeHealth -= SetEnemyHealth;
            _bot.OnSetBattleCards -= SetEnemyBattleCards;
            _player.OnFillHand -= ProcessHandFill;
            _player.OnWin -= ShowWin;
            _player.OnLose -= ShowLoose;
        }
        
        public void Tick()
        {
            if(_isLoaded == false)
                return;
            
            _room.Tick();
        }
        
        
        private void SetEnemyBattleCards() => _battleMediator.SetEnemyBattleCards(_bot.SetToBattle);

        private void ShowLoose() => Loose = true;

        private void ShowWin() => Win = true;

        private void SetEnemyHealth() => _battleMediator.SetEnemyHealth(_bot.Health, _bot.MaxHealth);
        private void SetPlayerHealth() => _battleMediator.SetPlayerHealth(_player.Health, _player.MaxHealth);
        private void InitBattleMediator() => _battleMediator.Init(_player.Deck.Count, _player.Discard.Count);
        private void MovePlayerBattleCardsToDiscard() => _battleMediator.MoveCardsToDiscard(_player.SetToBattle);
        private void MoveEnemyBattleCardsToDiscard() => _battleMediator.MoveCardsToDiscard(_bot.SetToBattle);
        private void MoveHandCardsToDiscard() => _battleMediator.MoveCardsToDiscard(_player.Hand);
    }
}