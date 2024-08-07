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
        public Sprite Sprite;
        public EffectType EffectType { get; set; }
        public List<StatType> EffectStats { get; set; }
    }
}