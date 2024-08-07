using CodeBase._Services.Randomizer;
using CodeBase._Services.StaticData;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Logic;
using CodeBase.UI.Mediators;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer.Unity;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory 
    {
        private readonly AssetProvider _assets;
        private readonly StaticDataService _staticData;
        private readonly RandomService _randomService;
        private readonly MediatorFactory _mediatorFactory;
        private readonly LifetimeScope _parentScope;
        private Level _level;

        public GameFactory(AssetProvider assets, StaticDataService staticData, RandomService randomService,
            MediatorFactory mediatorFactory, LifetimeScope parentScope)
        {
            _parentScope = parentScope;
            _assets = assets;
            _staticData = staticData;
            _randomService = randomService;
            _mediatorFactory = mediatorFactory;
        }

        public async UniTask WarmUp(Level level)
        {
            _level = level;
            GameObject roadGo = await _assets.Load<GameObject>(AssetAddress.RoadPiece);
            GameObject playerGo = await _assets.Load<GameObject>(AssetAddress.PlayerView);
        }

        public void Cleanup()
        {
            
        }
    }
}
