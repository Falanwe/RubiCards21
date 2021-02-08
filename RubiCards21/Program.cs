using System;

namespace RubiCards21
{

    enum Values { Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace }
    enum Colors { Spades, Hearts, Diamonds, Cubs };
    class Card : IComparable<Card>
    {
        public Values value;
        public Colors color;

        public Card(Values _value, Colors _color) { value = _value; color = _color; }

        public int CompareTo(Card other)
        {
            if (color > other.color)
                return 1;
            if (color < other.color)
                return -1;

            return value > other.value ? 1 : -1;
        }

        public override string ToString()
        {
            return $"{value} of {color}";
        }
    }
    class CardDeck : IComparer<Card>
    {
        public List<Card> Cards;

        public CardDeck()
        {
            Cards = new List<Card>();
            int numColors = Enum.GetNames(typeof(Colors)).Length;
            int numValues = Enum.GetNames(typeof(Values)).Length;

            for (int color = 0; color < numColors; color++)
            {
                for (int value = 0; value < numValues; value++)
                {
                    Cards.Add(new Card((Values)value, (Colors)color));
                }
            }
        }
        public int Compare(Card a, Card b)
        {
            if (a.value > b.value)
            {
                return 1;
            }
            if (a.value < b.value)
            {
                return -1;
            }
            return a.color > b.color ? 1 : -1;
        }
        public void CompareRandom()
        {
            Random rand = new Random();
            Card a = Cards[rand.Next(Cards.Count + 1)];
            Card b = Cards[rand.Next(Cards.Count + 1)];
            Console.WriteLine(a + " vs " + b + "\n");
            Console.WriteLine(a + " won !\n");
        }

        public void WriteToConsole()
        {
            foreach (Card card in Cards)
            {
                Console.WriteLine(card);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            CardDeck cardDeck = new CardDeck();
            //cardDeck.WriteToConsole();
            cardDeck.CompareRandom();
            Console.ReadKey();
        }
    }
}
