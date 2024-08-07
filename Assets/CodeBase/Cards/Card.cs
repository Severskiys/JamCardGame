using System;
using _Tools.PersistentValues;
using CodeBase.StaticData;

namespace CodeBase.Cards
{
    public class Card : ICard
    {
        public event Action OnChangeState;
        
        private CardData _data;
        private BoolDataValueSavable _selectState;

        public Card(CardData data)
        {
            _data = data;
            _selectState = new BoolDataValueSavable("Select_state_" + _data.Id);
        }

        public Card(Card card)
        {
            _data = card._data;
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

        public string PlayerId { get; set; }
        public string Name => _data.Name;
        public int Damage => _data.Damage;
        public CardType Type => _data.CardType;
        public void ChangeSelection() => IsSelected = !IsSelected;
        public CardData GetData() => _data;
    }
}