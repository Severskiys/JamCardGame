using System;
using System.Collections.Generic;
using CodeBase.StaticData;

namespace CodeBase.Cards
{
    public interface ICard
    {
        public EffectType EffectType { get; }
        public List<StatType> EffectStats { get; }
        public string PlayerId { get; }
        public string Name { get; }
        public int Damage { get; }
        public CardType Type { get; }
        bool IsSelected { get; }
        public void ChangeSelection();
        event Action OnChangeState;
        public void SetWin();
        public void SetLose();
        public void SetEqual();
    }
}