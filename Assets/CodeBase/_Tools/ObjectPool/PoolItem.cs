using System;
using UnityEngine;

namespace CodeBase._Tools.ObjectPool
{
    [SelectionBase]
    public abstract class PoolItem : MonoBehaviour, IPoolable
    {
        protected Action<PoolItem> ReleaseAction;
        public int ID { get; set; }
        public void SetReleaseAction(Action<PoolItem> releaseAction) => ReleaseAction = releaseAction;

        public virtual void OnTakeFromPool()
        {
        }

        public virtual void OnReturnToPool()
        {
        }
    }
}