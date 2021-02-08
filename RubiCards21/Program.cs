using System;
using System.Linq;

namespace RubiCards21
{
    class Program
    {
        static void Main(string[] args)
        {
            var standardDeck = new StandardDeck();

            standardDeck.AddDefaultCards();

            Console.WriteLine("Exemple deck:");
            Console.WriteLine(standardDeck);

            Console.WriteLine("OrderBy deck:");
            Console.WriteLine(standardDeck.OrderBy(x => x).ToDeck<StandardDeck>());

            var standardCard1 = new StandardCard(StandardCardValue.Queen, StandardCardSuit.Diamonds);
            var standardCard2 = new StandardCard(StandardCardValue.King, StandardCardSuit.Hearts);

            Console.WriteLine("Exemple card:");
            Console.WriteLine(standardCard1);

            Console.WriteLine("Battle card:");
            if (standardCard2 > standardCard1)
			{
                Console.WriteLine(standardCard2.ToString() + " wins against " + standardCard1.ToString());
            }
        }
    }
}
