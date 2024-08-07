using CodeBase._Services.StaticData;
using CodeBase._Tools.StateMachine;
using CodeBase.Logic;
using CodeBase.UI.Mediators;
using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.States
{
    public class LoadMenuState : ISelfCompleteState
    {
        public bool Complete { get; private set; }

        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly StaticDataService _staticData;
        private readonly MediatorFactory _mediatorFactory;

        public LoadMenuState(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, StaticDataService staticDataService, MediatorFactory mediatorFactory)
        {
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _staticData = staticDataService;
            _mediatorFactory = mediatorFactory;
        }

        public void OnEnter()
        {
            Complete = false;
            _sceneLoader.LoadGameScene(OnLoaded);
        }

        public void OnExit()
        {
            _loadingCurtain.Hide();
        }

        public void Tick()
        {
        }

        private async void OnLoaded()
        {
            await _mediatorFactory.Show<MenuMediator>();
            Complete = true;
        }
        
        private async UniTask InitGameWorld()
        {
            await UniTask.Yield();
        }
    }
}