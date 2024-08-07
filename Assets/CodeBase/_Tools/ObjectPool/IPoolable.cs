using System;

namespace CodeBase._Tools.ObjectPool
{
    public interface IPoolable
    {
        public int ID { get; set; }
        public void SetReleaseAction(Action<PoolItem> releaseAction);
        public void OnReturnToPool();
        public void OnTakeFromPool();
    }
}