using UnityEngine;

namespace _Tools.PersistentValues
{
	public class StringDataValueSavable : DataValueSavable<string>
	{
		public StringDataValueSavable(string saveKey) : base(saveKey)
		{
		}

		protected override void Load()
		{
			base.Load();
			Value = PlayerPrefs.GetString(SaveKey, "");
		}

		public override void Save()
		{
			base.Save();
			PlayerPrefs.SetString(SaveKey, Value);
		}
	}
}