using System;
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
    }
}