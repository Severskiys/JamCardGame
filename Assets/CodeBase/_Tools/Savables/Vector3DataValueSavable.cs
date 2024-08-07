using UnityEngine;

namespace _Tools.PersistentValues
{
    public class Vector3DataValueSavable : DataValueSavable<Vector3>
    {
        public Vector3DataValueSavable(string saveKey) : base(saveKey)
        {
        }

        protected override void Load()
        {
            base.Load();
            float x = PlayerPrefs.GetFloat(SaveKey + "_x", 0);
            float y = PlayerPrefs.GetFloat(SaveKey + "_y", 0);
            float z = PlayerPrefs.GetFloat(SaveKey + "_z", 0);
            Value = new Vector3(x, y, z);
        }

        public override void Save()
        {
            base.Save();
            PlayerPrefs.SetFloat(SaveKey + "_x", Value.x);
            PlayerPrefs.SetFloat(SaveKey + "_y", Value.y);
            PlayerPrefs.SetFloat(SaveKey + "_z", Value.z);
        }
        
        public override void Delete()
        {
            _isLoaded = false;
            PlayerPrefs.DeleteKey(SaveKey + "_x");
            PlayerPrefs.DeleteKey(SaveKey + "_y");
            PlayerPrefs.DeleteKey(SaveKey + "_z");
        }

        public override bool HasSaving() => PlayerPrefs.HasKey(SaveKey + "_x");
    }
}