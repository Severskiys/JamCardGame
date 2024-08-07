using System;
using CodeBase.StaticData;

namespace CodeBase.Cards
{
    public interface ICard
    {
        public string PlayerId { get; }
        public string Name { get; }
        public int Damage { get; }
        public CardType Type { get; }
        bool IsSelected { get; }
        public void ChangeSelection();
        public CardData GetData();
        event Action OnChangeState;
    }
}