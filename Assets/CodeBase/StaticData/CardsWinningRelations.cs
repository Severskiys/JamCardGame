using System;
using System.Collections.Generic;

namespace CodeBase.StaticData
{
    [Serializable]
    public class CardsWinningRelations
    {
        public CardType Winner;
        public List<CardType> Losers;
    }
}