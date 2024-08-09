using System;
using System.Collections.Generic;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Cards
{
    public interface ICard
    {
        event Action OnChangeState;
        public event Action OnShowWin;
        public event Action OnShowLose;
        public event Action OnShowEqual;
        public EffectType EffectType { get; }
        public List<SimpleStat> EffectStats { get; }
        public string PlayerId { get; }
        public string Name { get; }
        public int Damage { get; }
        public CardType Type { get; }
        bool IsSelected { get; }
        string Id { get; }
        public void ChangeSelection();
        public void SetWin();
        public void SetLose();
        public void SetEqual();
        public bool TrySetInBattleSlot(int battleSlotIndex);
        public float GetEffectStat(StatType amount);
        public bool IsBanned { get; set; }
    }
}