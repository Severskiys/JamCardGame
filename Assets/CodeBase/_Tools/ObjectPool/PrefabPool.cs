using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase._Tools.ObjectPool
{
    public class PrefabPool<T> where T : Component
    {
        private T[] _items = new T[512];
        private int _itemsCount;
        private readonly T _prefab;

        public PrefabPool(string prefabPath)
        {
            var asset = Resources.Load<GameObject>(prefabPath);
#if DEBUG
            if (asset == false) throw new Exception($"asset from path \"{prefabPath}\" not found.");
#endif
            _prefab = asset.GetComponent<T>();
            if (_prefab == false)
                _prefab = Object.Instantiate(asset).AddComponent<T>();
            
            _prefab.gameObject.SetActive(false);
        }

        public PrefabPool(T prefab)
        {
            _prefab = Object.Instantiate(prefab);
            _prefab.gameObject.SetActive(false);
        }

        public T Get()
        {
            if (_itemsCount > 0)
            {
                return _items[--_itemsCount];
            }

            return Object.Instantiate(_prefab);
        }

        public void Recycle(T item)
        {
#if DEBUG
            if (!item)
            {
                throw new Exception("item is null.");
            }
#endif
            if (_itemsCount == _items.Length)
            {
                Array.Resize(ref _items, _itemsCount << 1);
            }

            item.transform.SetParent(null);
            item.gameObject.SetActive(false);
            _items[_itemsCount++] = item;
        }
    }
}
