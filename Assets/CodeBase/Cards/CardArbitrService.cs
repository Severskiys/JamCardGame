using System.Collections.Generic;
using System.Linq;
using CodeBase._Services.StaticData;
using CodeBase.StaticData;

namespace CodeBase.Cards
{
    public class CardArbiterService
    {
        private Dictionary<CardType, List<CardType>> CardRelationsMap = new Dictionary<CardType, List<CardType>>();

        public CardArbiterService(StaticDataService staticDataService)
        {
            List<CardsWinningRelations> cardsWinningRelations = staticDataService.CardsRelations();
            foreach (var cardRelation in cardsWinningRelations)
                CardRelationsMap.Add(cardRelation.Winner, cardRelation.Losers);
        }

        public (bool hasWinner, ICard winner) DetermineWinner(ICard card1, ICard card2)
        {
            if (CardRelationsMap[card1.Type].Any(loser => loser == card2.Type))
                return (true, card1);
            
            if (CardRelationsMap[card2.Type].Any(loser => loser == card1.Type))
                return (true, card2);
            
            return (false, null);
        }
    }
}