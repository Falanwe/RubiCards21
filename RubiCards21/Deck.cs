using System;
using System.Collections.Generic;
using System.Text;

namespace CardBattle
{
    class Deck
    {
        protected List<Card> cards;
        public Deck Discard { get; set; }
        private Random random = new Random();

        private Deck()
        {
            cards = new List<Card>();
        }

        public static Deck CreateDeck()
        {
            Deck deck = new Deck();
            deck.InsertCardsOnTop(new Card[] {
                new Card("Deux de Trefle", 11), new Card("Deux de Carreau", 12), new Card("Deux de Coeur", 13),  new Card("Deux de Pique", 14),
                new Card("Trois de Trefle", 21), new Card("Trois de Carreau", 22), new Card("Trois de Coeur", 23), new Card("Trois de Pique", 24),
                new Card("Quatre de Trefle", 31), new Card("Quatre de Carreau", 32), new Card("Quatre de Coeur", 33), new Card("Quatre de Pique", 34),
                new Card("Cinq de Trefle", 41), new Card("Cinq de Carreau", 42), new Card("Cinq de Coeur", 43), new Card("Cinq de Pique", 44),
                new Card("Six de Trefle", 51), new Card("Six de Carreau", 52), new Card("Six de Coeur", 53), new Card("Six de Pique", 54),
                new Card("Sept de Trefle", 61), new Card("Sept de Carreau", 62), new Card("Sept de Coeur", 63), new Card("Sept de Pique", 64),
                new Card("Huit de Trefle", 71), new Card("Huit de Carreau", 72), new Card("Huit de Coeur", 73), new Card("Huit de Pique", 74),
                new Card("Neuf de Trefle", 81), new Card("Neuf de Carreau", 82), new Card("Neuf de Coeur", 83), new Card("Neuf de Pique", 84),
                new Card("Dix de Trefle", 91), new Card("Dix de Carreau", 92), new Card("Dix de Coeur", 93), new Card("Dix de Pique", 94),
                new Card("Valet de Trefle", 101), new Card("Valet de Carreau", 102), new Card("Valet de Coeur", 103), new Card("Valet de Pique", 104),
                new Card("Dame de Trefle", 111), new Card("Dame de Carreau", 112), new Card("Dame de Coeur", 113), new Card("Dame de Pique", 114),
                new Card("Roi de Trefle", 121), new Card("Roi de Carreau", 122), new Card("Roi de Coeur", 123), new Card("Roi de Pique", 124),
                new Card("As de Trefle", 131), new Card("As de Carreau", 132), new Card("As de Coeur", 133), new Card("As de Pique", 134)
            });
            deck.Discard = Deck.CreateEmpty();
            return deck;
        }

        public static Deck CreateEmpty()
        {
            return new Deck();
        }

        public List<Card> GetCards() { return cards; }

        public Card GetCard(int index)
        {
            if (index < 0 || index >= cards.Count) throw new Exception("Impossible to get the card at index " + index + " there isn't that amount of card in the deck.");
            Card card = cards[index];
            cards.RemoveAt(index);
            if (Discard.InsertTop(card) == 0) return card;
            else throw new Exception("Cannot insert in discard the card " + card);
        }

        public Card GetTop()
        {
            return GetCard(0);
        }

        public Card GetLast()
        {
            return GetCard(cards.Count - 1);
        }

        public int InsertCard(int index, Card card)
        {
            try
            {
                cards.Insert(index, card);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
                return 1;
            };
            return 0;
        }

        public int InsertCards(int index, IEnumerable<Card> cards)
        {
            try
            {
                this.cards.InsertRange(index, cards);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
                return 1;
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
                return 2;
            };
            return 0;
        }

        public int InsertTop(Card card)
        {
            return InsertCard(0, card);
        }

        public int InsertCardsOnTop(IEnumerable<Card> cards)
        {
            return InsertCards(0, cards);
        }

        public int InsertLast(Card card)
        {
            return InsertCard(this.cards.Count, card);
        }

        public int InsertCardsOnBottom(IEnumerable<Card> cards)
        {
            return InsertCards(this.cards.Count, cards);
        }

        public void Shuffle()
        {
            for (int i = cards.Count; i > 0; i--)
                Swap(0, random.Next(0, i));
        }

        public void Swap(int first, int second)
        {
            Card temp = cards[first];
            cards[first] = cards[second];
            cards[second] = temp;
        }

        public void Restock()
        {
            if (InsertCardsOnBottom(Discard.GetCards()) > 0) throw new Exception("Impossible to restock discard into the bottom of the deck");
            else Discard.GetCards().Clear();
        }

        public int Count()
        {
            return cards.Count;
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < cards.Count; i++) s += (i + 1) + " | " + cards[i] + "\n";
            return s;
        }
    }
}
