using System.Collections.Generic;
using System.Linq;
using CodeBase._Services.StaticData;
using CodeBase._Tools.Helpers;
using CodeBase._Tools.StateMachine;
using CodeBase.Cards;
using CodeBase.Logic;
using CodeBase.StaticData;

namespace CodeBase.Battle
{
    public class BotBattleRoom : IBattleRoom, IPlayersHolder
    {
        private readonly Dictionary<string, IPlayer> _players = new();
        private readonly List<BattleSlot> _slots = new();
        private readonly CardsService _cardsService;
        private readonly StateMachine _battleStateMachine;
        private readonly ISelfCompleteState _prepareHandState;
        private readonly GameData _gameData;

        public List<IPlayer> BattlePlayers => _players.Values.ToList();
        
        public BotBattleRoom(CardsService cardsService, StaticDataService staticDataService, CardArbiterService cardArbiterService)
        {
            _gameData = staticDataService.GameData();
            _cardsService = cardsService;
            _players.Clear();
            _slots.Clear();
            
            for (int i = 0; i < staticDataService.GameData().BattleSlotsCount; i++)
                _slots.Add(new BattleSlot());
            
            _battleStateMachine = new StateMachine();
            _prepareHandState = new PrepareHandsState(this);
            IState pendingState = new PendingState();
            ISelfCompleteState waitPlayersInputState = new WaitPlayerInputState(_slots, this);
            ISelfCompleteState clearHandsState = new ClearHandsState(this);
            ApplyCardsEffectsState applyCardsEffectsState = new ApplyCardsEffectsState(this, _slots);
            ResolveCardsInteractionState resolveCardsInteractionState = new ResolveCardsInteractionState(this, _slots, cardArbiterService);
            ISelfCompleteState processEndOfTurnEffects = new ProcessEndOfTurnEffects(this);
            CheckBattleEndState checkBattleEndState = new CheckBattleEndState(this);
            
            _battleStateMachine.AddTransition(_prepareHandState, waitPlayersInputState, ()=> _prepareHandState.Complete);
            _battleStateMachine.AddTransition(waitPlayersInputState, applyCardsEffectsState, ()=> waitPlayersInputState.Complete);
            _battleStateMachine.AddTransition(applyCardsEffectsState, resolveCardsInteractionState, ()=> applyCardsEffectsState.Complete);
            _battleStateMachine.AddTransition(resolveCardsInteractionState, waitPlayersInputState, ()=> resolveCardsInteractionState.Complete && resolveCardsInteractionState.EnterStateCount%_gameData.OneRoundLength != 0 && _players.All(p=>p.Value.IsAlive));
            _battleStateMachine.AddTransition(resolveCardsInteractionState, clearHandsState, ()=> resolveCardsInteractionState.Complete && resolveCardsInteractionState.EnterStateCount%_gameData.OneRoundLength == 0 && _players.All(p=>p.Value.IsAlive));
            _battleStateMachine.AddTransition(clearHandsState, processEndOfTurnEffects, ()=> clearHandsState.Complete);
            _battleStateMachine.AddTransition(processEndOfTurnEffects, _prepareHandState, ()=> processEndOfTurnEffects.Complete);
            _battleStateMachine.AddAnyTransition(checkBattleEndState, ()=> _players.Any(p=>p.Value.IsAlive == false) && resolveCardsInteractionState.Complete);
            _battleStateMachine.SetState(pendingState);
        }
        
        public BattlePlayer CreatePlayer()
        {
            string id = UniqueId.NewId();
            var player = new BattlePlayer(_cardsService.SelectedCards, id, _gameData.PlayerHealth, _gameData.HandSize, this);
            _players.Add(id, player);
            return player;
        }

        public BattlePlayer CreateBot()
        {
            string botId = UniqueId.NewId();
            List<ICard> shuffledCards = _cardsService.AllCards.Randomize().ToList();
            shuffledCards = shuffledCards.GetRange(0, _cardsService.DeckSize);
            BattleBot bot = new BattleBot(shuffledCards, botId, _gameData.PlayerHealth, _gameData.HandSize, this, _slots);
            _players.Add(botId, bot);
            return bot;
        }

        public void StartBattle() => _battleStateMachine.SetState(_prepareHandState);
        
        public bool TrySetCard(ICard card, int slotIndex) => _slots[slotIndex].PutCard(card);

        public void Tick()
        {
            _battleStateMachine.Tick();
            _battleStateMachine.LogState();
        }

        public void Dispose()
        {
            _players.Clear();
            _slots.Clear();
        }
    }
}