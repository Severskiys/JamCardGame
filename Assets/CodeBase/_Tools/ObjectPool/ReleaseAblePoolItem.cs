using UnityEngine;

namespace CodeBase._Tools.ObjectPool
{
    [SelectionBase]
    public abstract class ReleaseAblePoolItem : PoolItem
    {
        public void Release() => ReleaseAction.Invoke(this);
    }
}