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
        private LoadBattleState _loadBattleState;

        public Game(IObjectResolver resolver)
        {
            _stateMachine = new StateMachine();
            _loadMenuState = resolver.Resolve<LoadMenuState>();
            _loadBattleState = resolver.Resolve<LoadBattleState>();
            _stateMachine.AddTransition(_loadMenuState, resolver.Resolve<MenuState>(), () => _loadMenuState.Complete);
            _stateMachine.AddTransition(_loadBattleState, resolver.Resolve<BattleLoopState>(), () => _loadBattleState.Complete);
        }

        public void LoadGameLevel()
        {
            _stateMachine.SetState(_loadBattleState);
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
