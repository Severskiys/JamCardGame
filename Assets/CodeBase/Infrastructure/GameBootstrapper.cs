using CodeBase._Services.Input;
using CodeBase._Services.Randomizer;
using CodeBase._Services.StaticData;
using CodeBase.Cards;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.States;
using CodeBase.Logic;
using CodeBase.UI.Mediators;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : LifetimeScope
    {
        [SerializeField] private LoadingCurtain _loadingCurtain;
        
        protected override void Configure(IContainerBuilder builder)
        {
            _loadingCurtain.Show();
            DontDestroyOnLoad(gameObject);
            builder.RegisterInstance(_loadingCurtain);
            builder.RegisterInstance(InputService());
            builder.Register<AssetProvider>(Lifetime.Singleton);
            builder.Register<SceneLoader>(Lifetime.Singleton);
            builder.Register<StaticDataService>(Lifetime.Singleton);
            builder.Register<CardsService>(Lifetime.Singleton);
            builder.Register<CardArbiterService>(Lifetime.Singleton);
            builder.Register<RandomService>(Lifetime.Singleton);
            builder.Register<GameFactory>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<MediatorFactory>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<BattleState>(Lifetime.Singleton);
            builder.Register<LoadMenuState>(Lifetime.Singleton);
            builder.Register<MenuState>(Lifetime.Singleton);
            builder.RegisterEntryPoint<Game>();
        }
        
        private static IInputService InputService() => Application.isEditor
            ? new StandaloneInputService()
            : new MobileInputService();
    }
}
