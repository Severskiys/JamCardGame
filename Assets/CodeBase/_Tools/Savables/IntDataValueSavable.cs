using UnityEngine;

namespace _Tools.PersistentValues
{
    public class IntDataValueSavable : DataValueSavable<int>
    {
        public IntDataValueSavable(string saveKey) : base(saveKey)
        {
        }

        protected override void Load()
        {
            base.Load();
            Value = PlayerPrefs.GetInt(SaveKey, 0);
        }

        public override void Save()
        {
            base.Save();
            PlayerPrefs.SetInt(SaveKey, Value);
        }
    }
}