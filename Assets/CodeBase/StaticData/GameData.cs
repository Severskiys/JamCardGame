using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "GameData", menuName = "StaticData/GameData")]
    public class GameData : ScriptableObject
    {
        public int DeckSize;
        public int PlayerHealth;
        public int HandSize;
        public bool DiscardUnselectedCards;
        public int BattleSlotsCount;
        public List<CardData> CardDatas;
        public List<CardsWinningRelations> CardsRelations;

        private void OnValidate()
        {
            for (var index = 0; index < CardDatas.Count; index++)
            {
                var card = CardDatas[index];
                card.Id = card.Id.Trim();
                if (card.Id.Equals(""))
                    card.Id = Guid.NewGuid().ToString();
                else if (index > 0 && card.Id == CardDatas[index - 1].Id)
                    card.Id = Guid.NewGuid().ToString();
            }
        }
    }
}