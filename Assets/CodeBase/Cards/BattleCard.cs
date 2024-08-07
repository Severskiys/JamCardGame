using System;
using CodeBase.StaticData;

namespace CodeBase.Cards
{
    public class BattleCard : ICard
    {       
        public event Action OnChangeState;
        public bool IsSelected { get; set; }
        
        private readonly CardData _data;
        
        public BattleCard(ICard card, string playerId)
        {
            IsSelected = true;
            _data = card.GetData();
            PlayerId = playerId;
        }
        
        public void ChangeSelection()
        {
            
        }
        
        public string PlayerId { get;}
        public string Name => _data.Name;
        public int Damage => _data.Damage;
        public CardType Type => _data.CardType;
        


        public CardData GetData() => _data;

    }
}