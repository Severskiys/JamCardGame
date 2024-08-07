using System.Collections.Generic;
using System.Linq;
using CodeBase.Cards;

namespace CodeBase.Battle
{
    public class BattleSlot
    {
        private const int Capacity = 2;
        private Dictionary<string, ICard> _cardsMap = new ();
        private int _cardsCount;
        
        public List<ICard> CardsInSlot => _cardsMap.Values.ToList();
        public BattleSlot()
        {
            ClearSlot();
        }

        public bool PutCard(ICard card)
        {
            if (_cardsMap.ContainsKey(card.PlayerId) == false)
            {
                _cardsMap.Add(card.PlayerId, card);
                _cardsCount += 1;
                return true;
            }

            return false;
        }

        public void ClearSlot()
        {
            _cardsMap.Clear();
            _cardsCount = 0;
        }

        public bool FullyEquipped => _cardsCount >= Capacity;
    }
}