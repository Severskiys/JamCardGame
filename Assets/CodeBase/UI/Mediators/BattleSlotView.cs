using UnityEngine;

namespace CodeBase.UI.Mediators
{
    public class BattleSlotView : MonoBehaviour
    {
        public void Init(int index)
        {
            Index = index;
        }

        public int Index { get; private set; }
    }
}