using System.Collections.Generic;
using System.Linq;

namespace RubiCards21
{
    public class CardHand : CardContainer
    {
        public CardHand(Card[] cards) : base(cards)
        {
        }

        public CardHand(uint numberOfRandomCards) : base(numberOfRandomCards)
        {
        }
    }
}