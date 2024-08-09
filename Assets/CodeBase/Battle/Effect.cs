using System;
using System.Collections.Generic;
using CodeBase.Cards;
using CodeBase.StaticData;

namespace CodeBase.Battle
{
    public class Effect : IEffect
    {
        private readonly Dictionary<StatType, float> _statsMap = new();
        
        public Effect(EffectType type, List<SimpleStat> stats)
        {
            Type = type;
            foreach (var stat in stats)
                _statsMap.Add(stat.Type, stat.Value);
        }
        
        public EffectType Type { get; }
        public float GetStat(StatType type) => _statsMap[type];
        public void TryAddDuration(IEffect effect)
        {
            if (_statsMap.ContainsKey(StatType.TurnsCount))
                _statsMap[StatType.TurnsCount] += effect.GetStat(StatType.TurnsCount);
        }
    }
}