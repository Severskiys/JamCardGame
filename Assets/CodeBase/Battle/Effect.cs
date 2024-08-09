using System;
using System.Collections.Generic;
using CodeBase.Cards;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Battle
{
    public class Effect : IEffect
    {
        private readonly Dictionary<StatType, float> _statsMap = new();
        private int _turnsCount;
        
        public Effect(EffectType type, List<SimpleStat> stats)
        {
            Type = type;
            foreach (var stat in stats)
                _statsMap.Add(stat.Type, stat.Value);

            if (_statsMap.TryGetValue(StatType.TurnsCount, out var value))
                _turnsCount = Mathf.CeilToInt(value);
        }
        
        public EffectType Type { get; }
        public bool CheckExpired()
        {
            _turnsCount -= 1;
            return _statsMap.ContainsKey(StatType.TurnsCount) && _turnsCount > 0;
        }

        public float GetStat(StatType type) => _statsMap[type];
        public void TryAddDuration(IEffect effect)
        {
            if (_statsMap.ContainsKey(StatType.TurnsCount))
                _statsMap[StatType.TurnsCount] += effect.GetStat(StatType.TurnsCount);
        }
    }
}