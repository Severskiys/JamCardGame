using System.Collections.Generic;
using System.Linq;
using CodeBase._Services.StaticData;

namespace CodeBase.Cards
{
    public class CardsService
    {
        private readonly List<ICard> _allCards = new();

        public CardsService(StaticDataService staticData)
        {
            _allCards.Clear();
            HandSize = staticData.GameData().HandSize;
            DeckSize = staticData.GameData().DeckSize;
            var cardsList = staticData.CardsList();
            foreach (var cardData in cardsList)
            {
                var card = new Card(cardData);
                _allCards.Add(card);
            }
        }

        public int HandSize { get; }
        public int DeckSize { get; }
        public List<ICard> AllCards => _allCards;
        public List<ICard> SelectedCards => AllCards.Where(c=>c.IsSelected).ToList();
        public int SelectedCardsCount => AllCards.Count(c => c.IsSelected);

        public void TryChangeCardSelection(ICard clickedCard)
        {
            if (clickedCard.IsSelected)
            {
                clickedCard.ChangeSelection();
            }
            else if (SelectedCardsCount < DeckSize)
            {
                clickedCard.ChangeSelection();
            }
        }
    }
}