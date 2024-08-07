using System;
using UnityEngine;

namespace _Tools.PersistentValues
{
    public class DataValueSavable<T>
    {
        public event Action<T> OnChangeValue;

        private T _value;
        protected bool _isLoaded;

        protected readonly string SaveKey;

        public DataValueSavable(string saveKey)
        {
            SaveKey = saveKey;
        }

        public T Value
        {
            get
            {
                if (_isLoaded == false)
                    Load();

                return _value;
            }

            set
            {
                _value = value;
                OnChangeValue?.Invoke(_value);
            }
        }

        protected virtual void Load()
        {
            _isLoaded = true;
        }

        public virtual void Save()
        {
            _isLoaded = true;
        }

        public virtual void Delete()
        {
            _isLoaded = false;
            PlayerPrefs.DeleteKey(SaveKey);
        }

        public virtual bool HasSaving() => PlayerPrefs.HasKey(SaveKey);
    }
}