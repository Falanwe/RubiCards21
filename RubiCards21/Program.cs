using System;

namespace CardBattle
{
    class Program
    {
        static void Main(string[] args)
        {
            Deck deck = Deck.CreateDeck();

            deck.Shuffle();

            Console.WriteLine(deck);
            Console.WriteLine("");

            Card a = deck.GetTop();
            Card b = deck.GetTop();
            Card c = deck.GetLast();

            Console.WriteLine(deck);
            Console.WriteLine("");

            if ( a.CompareTo(b) < 0 )
            {
                Console.WriteLine("A: " + a + "  < B: " + b);
            } else if (a.CompareTo(b) > 0)
            {
                Console.WriteLine("A: " + a + "  > B: " + b);
            } else
            {
                Console.Write("A: " + a + "  == B: " + b);
            }

            deck.Restock();

            Console.WriteLine(deck);
            Console.WriteLine("");
        }
    }
}
