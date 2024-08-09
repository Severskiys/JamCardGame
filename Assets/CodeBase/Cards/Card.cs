using System;
using System.Collections.Generic;
using System.Linq;
using _Tools.PersistentValues;
using CodeBase.StaticData;

namespace CodeBase.Cards
{
    public class Card : ICard
    {
        public event Action OnChangeState;
        public event Action OnShowWin;
        public event Action OnShowLose;
        public event Action OnShowEqual;
        private readonly CardData _data;
        private readonly BoolDataValueSavable _selectState;

        public EffectType EffectType => _data.EffectType;
        public List<SimpleStat> EffectStats => _data.EffectStats;
        public string PlayerId { get; set; }
        public string Name => _data.Name;
        public int Damage => _data.Damage;
        public CardType Type => _data.CardType;
        public string Id => _data.Id;

        public Card(CardData data)
        {
            _data = data;
            _selectState = new BoolDataValueSavable("Select_state_" + _data.Id);
        }

        public bool IsSelected
        {
            get => _selectState.Value;
            private set
            {
                _selectState.Value = value;
                _selectState.Save();
                OnChangeState?.Invoke();
            }
        }

        public void ChangeSelection() => IsSelected = !IsSelected;

        public void SetWin()
        {
        }

        public void SetLose()
        {
        }

        public void SetEqual()
        {
        }

        public bool TrySetInBattleSlot(int battleSlotIndex) => false;
        public float GetEffectStat(StatType type) => _data.EffectStats.FirstOrDefault(es => es.Type == type)!.Value;
        public bool IsBanned { get; set; }
    }
}