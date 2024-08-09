using System;
using System.Collections.Generic;
using CodeBase.Cards;
using UnityEngine;

namespace CodeBase.StaticData
{
    [Serializable]
    public class CardData
    {
        public string Name;
        public string Id;
        public CardType CardType;
        public int Damage;
        public EffectType EffectType;
        public List<SimpleStat> EffectStats;
    }
    
    [Serializable]
    public class SimpleStat
    {
        public StatType Type;
        public float Value;
    }
}