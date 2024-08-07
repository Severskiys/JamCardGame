using System.Collections.Generic;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase._Services.StaticData
{
    public class StaticDataService : IService
    {
        private const string GameDataPath = "GameData";
        private readonly GameData _gameData;
        private int _enemiesCount;
        public StaticDataService() => _gameData = Resources.Load<GameData>(GameDataPath);
        public List<CardData> CardsList() => _gameData.CardDatas;
        public GameData GameData() => _gameData;
        public int GetHealth() => _gameData.PlayerHealth;
        public List<CardsWinningRelations> CardsRelations() =>  _gameData.CardsRelations;
    }
}
