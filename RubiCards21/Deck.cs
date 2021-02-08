namespace RubiCards21
{
    public class Deck : CardContainer
    {
        public Deck(Card[] cards) : base(cards)
        {
        }

        public Deck(uint numberOfRandomCards) : base(numberOfRandomCards)
        {
        }
    }
}