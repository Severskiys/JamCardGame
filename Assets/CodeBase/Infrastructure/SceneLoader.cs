using System;
using CodeBase.Infrastructure.AssetManagement;

namespace CodeBase.Infrastructure
{
    public class SceneLoader
    {
        private readonly AssetProvider _assetProvider;

        public SceneLoader(AssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public async void LoadGameScene(Action onLoaded = null)
        {
            await _assetProvider.LoadSceneSingle("MainScene");
            onLoaded?.Invoke();
        }
    }
}