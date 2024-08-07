using UnityEngine;

namespace _Tools.PersistentValues
{
	public class BoolDataValueSavable : DataValueSavable<bool>
	{
		public BoolDataValueSavable(string saveKey) : base(saveKey)
		{
		}

		protected override void Load()
		{
			base.Load();
			Value = PlayerPrefs.GetInt(SaveKey, 0) == 1;
		}

		public override void Save()
		{
			base.Save();
			PlayerPrefs.SetInt(SaveKey, Value ? 1 : 0);
		}
	}
}