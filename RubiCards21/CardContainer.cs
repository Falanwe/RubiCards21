using System.Collections.Generic;
using System.Linq;

namespace RubiCards21
{
    public class CardContainer
    {
        public List<Card> Cards { get; private set; } = new List<Card>();

        #region Constructors
        public CardContainer(Card[] cards)
        {
            Cards = cards.ToList();
        }

        public CardContainer(uint numberOfRandomCards)
        {
            for (int i = 0; i < numberOfRandomCards; i++)
            {
                AddCard(Card.Random());
            }
        }
        #endregion

        public void AddCard(Card card) => Cards.Add(card);

        public void RemoveCard(uint cardIndex)
        {
            if (!IsValidIndex(cardIndex)) return;
            
            Cards.RemoveAt((int)cardIndex);
        }

        public void ResetHand() => Cards.Clear();
        public void ResetHand(Card[] newCards) => Cards = newCards.ToList();

        public void Draw(CardContainer fromContainer, uint cardIndex)
        {
            if (!IsValidIndex(cardIndex)) return;
            
            Cards.Add(fromContainer.Cards[(int)cardIndex]);
            fromContainer.Cards.RemoveAt((int)cardIndex);
        }

        public void DrawFromTop(CardContainer fromContainer, int numberOfCards = 1)
        {
            if (fromContainer.Cards.Count < numberOfCards) return;

            for (int i = 0; i < numberOfCards; i++)
            {
                Draw(fromContainer, 0);
            }
        }

        public void DrawRandom(CardContainer fromContainer, uint numberOfCards = 1)
        {
            if (fromContainer.Cards.Count < numberOfCards) return;
            
            for (int i = 0; i < numberOfCards; i++)
            {
                Draw(fromContainer, (uint)CardGameUtility.GetRandomIndex(fromContainer.Cards));
            }
        }

        public bool IsEmpty() => Cards.Count == 0;

        public bool IsValidIndex(uint index) => index - 1 <= Cards.Count;
    }
}