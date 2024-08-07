using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase._Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.AssetManagement
{
    public class AssetProvider : IService
    {
        private readonly Dictionary<string, Object> _completedCashe = new();
        
        public async UniTask<T> Load<T>(string address) where T : Object
        {
            if (_completedCashe.TryGetValue(address, out Object loadedObject)) 
                return loadedObject as T;
            
            Object result = await Resources.LoadAsync<T>(address).ToUniTask();
            _completedCashe.TryAdd(address, result);
            return result as T;
        }
        
        public void Cleanup() => _completedCashe.Clear();
        public async Task LoadSceneSingle(string sceneName) => await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }
}