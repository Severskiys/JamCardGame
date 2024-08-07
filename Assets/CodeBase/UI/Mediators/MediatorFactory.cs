using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace CodeBase.UI.Mediators
{
    public class MediatorFactory
    {
        private const string UIRootPath = "UiRoot";
        private readonly AssetProvider _assets;
        private readonly Dictionary<Type, IMediator> _mediators;
        private readonly LifetimeScope _parentScope;
        private Transform _uiRoot;

        public MediatorFactory(AssetProvider assets, LifetimeScope parentScope)
        {
            _parentScope = parentScope;
            _assets = assets;
            _mediators = new Dictionary<Type, IMediator>();
        }

        private async UniTask CreateUIRoot()
        {
            GameObject prefab = await _assets.Load<GameObject>(UIRootPath);
            GameObject root = Object.Instantiate(prefab);
            _uiRoot = root.transform;
        }

        private async UniTask<TMediator> Get<TMediator>() where TMediator : MonoBehaviour, IMediator
        {
            if (_mediators.TryGetValue(typeof(TMediator), out var mediator))
                return mediator as TMediator;
            
            if (_uiRoot == default)
                await CreateUIRoot();

            TMediator mediatorGo = await _assets.Load<TMediator>(typeof(TMediator).Name);
            TMediator instance = Object.Instantiate(mediatorGo, _uiRoot);
            _parentScope.Container.Inject(instance);
            _mediators.Add(instance.GetType(), instance);
            instance.OnCleanUp += CleanUp;
            return instance;
        }
        
        public async UniTask Show<TMediator>() where TMediator : MonoBehaviour, IMediator
        {
            var result = await Get<TMediator>();
            foreach (var mediatorPair in _mediators)
                mediatorPair.Value.Hide();
            result.Show();
        }

        private void CleanUp(IMediator mediator)
        {
            mediator.OnCleanUp -= CleanUp;
            if (_mediators.ContainsKey(mediator.GetType()))
                _mediators.Remove(mediator.GetType());
        }

        public void CleanupAll()
        {
            _uiRoot = default;
            _assets.Cleanup();
        }
    }
}
