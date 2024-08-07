using CodeBase._Services.StaticData;
using CodeBase._Tools.StateMachine;
using CodeBase.Logic;
using CodeBase.UI.Mediators;

namespace CodeBase.Infrastructure.States
{
    public class LoadBattleState : ISelfCompleteState
    {
        public bool Complete { get; private set; }

        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly StaticDataService _staticData;
        private readonly MediatorFactory _mediatorFactory;

        public LoadBattleState(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, StaticDataService staticDataService, MediatorFactory mediatorFactory)
        {
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _staticData = staticDataService;
            _mediatorFactory = mediatorFactory;
        }

        public void OnEnter()
        {

        }

        public void OnExit()
        {
            
        }

        public void Tick()
        {
        }
    }
}