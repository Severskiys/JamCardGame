using System.Collections.Generic;
using UnityEngine;

namespace CodeBase._Tools.ObjectPool
{
    public class Pool : MonoBehaviour
    {
        private readonly Dictionary<int, Queue<IPoolable>> _poolItems = new();
        private readonly Dictionary<int, Transform> _containers = new();
        private readonly HashSet<IPoolable> _usedItems = new();
        
        public T Get<T>(T prefab, Transform parent = default) where T : MonoBehaviour, IPoolable
        {
            T pooledItem;
            int id = prefab.GetInstanceID();
            Transform container = GetContainer(id);
            Queue<IPoolable> queue = GetQueue(id);

            if (queue.Count == 0)
            {
                Transform newParent = parent == default ? container : parent;
                pooledItem = InstantiateObject(prefab, newParent, id);
                DoOnGet(prefab, pooledItem, container, 0);
                return pooledItem;
            }
            
            pooledItem = (T)queue.Dequeue();
            if (parent != default) pooledItem.transform.SetParent(parent, false);
            pooledItem.gameObject.SetActive(true);
            DoOnGet(prefab, pooledItem, container, queue.Count);
            return pooledItem;
        }

        public void Release<T>(T poolItem) where T : MonoBehaviour, IPoolable
        {
            Queue<IPoolable> queue = GetQueue(poolItem.ID);
            if (queue.Contains(poolItem))
                return;
            
            queue.Enqueue(poolItem);
            poolItem.OnReturnToPool();
            _usedItems.Remove(poolItem);
            Transform container = GetContainer(poolItem.ID);
            poolItem.transform.SetParent(container);
            UpdateContainerName(container, queue.Count, poolItem.name);
            poolItem.gameObject.SetActive(false);
        }

        private void DoOnGet<T>(T prefab, T pooledItem, Transform container, int itemsCount) where T : MonoBehaviour, IPoolable
        {
            pooledItem.OnTakeFromPool();
            _usedItems.Add(pooledItem);
            UpdateContainerName(container, itemsCount, prefab.name);
        }

        private T InstantiateObject<T>(T prefab,Transform newParent, int id) where T : MonoBehaviour, IPoolable
        {
            T instance = Instantiate(prefab, newParent);
            instance.name = prefab.name;
            instance.ID = id;
            instance.SetReleaseAction(Release);
            return instance;
        }

        private Queue<IPoolable> GetQueue(int id)
        {
            if (_poolItems.TryGetValue(id, out var queue))
                return queue;

            queue = new Queue<IPoolable>();
            _poolItems.Add(id, queue);
            return queue;
        }

        private Transform GetContainer(int id)
        {
            if (_containers.TryGetValue(id, out var container))
                return container;

            container = new GameObject().transform;
            container.parent = transform;
            _containers.Add(id, container);
            return container;
        }

        private void UpdateContainerName(Transform container, int pooled, string nameToSet = default)
        {
#if UNITY_EDITOR
            var newName = nameToSet ?? container.name;
            if (nameToSet != default) container.name = $"{newName}\t{pooled}/{container.childCount}";
#endif
        }
        
        private void OnDestroy()
        {
            _poolItems.Clear();
            _containers.Clear();
            _usedItems.Clear();
        }
    }
}