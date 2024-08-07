using System.Threading;
using CodeBase._Tools.StateMachine;
using CodeBase.Infrastructure.States;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Infrastructure
{
    public class Game : IGameplayStarter, ITickable, IStartable
    {
        private readonly StateMachine _stateMachine;
        private readonly LoadMenuState _loadMenuState;
        private BattleState _battleState;

        public Game(IObjectResolver resolver)
        {
            _stateMachine = new StateMachine();
            _loadMenuState = resolver.Resolve<LoadMenuState>();
            _battleState = resolver.Resolve<BattleState>();
            _stateMachine.AddTransition(_loadMenuState, resolver.Resolve<MenuState>(), () => _loadMenuState.Complete);
        }

        public void LoadGameLevel()
        {
            _stateMachine.SetState(_battleState);
        }

        public void Start()
        {
            _stateMachine.SetState(_loadMenuState);
        }

        public void Tick()
        {
            _stateMachine.Tick();
        }
    }
}
