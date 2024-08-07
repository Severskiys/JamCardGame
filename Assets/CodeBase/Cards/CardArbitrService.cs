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

        public (bool hasWinner, string playerId) DetermineWinner(List<ICard> cards)
        {
            if (CardRelationsMap[cards[1].Type].Any(loser => loser == cards[2].Type))
                return (true, cards[1].PlayerId);
            
            if (CardRelationsMap[cards[2].Type].Any(loser => loser == cards[1].Type))
                return (true, cards[2].PlayerId);
            
            return (false, "");
        }
    }
}