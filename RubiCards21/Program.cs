using System;

namespace RubiCards21
{
    class Program
    {
        static void Main(string[] args)
        {
            var hand = new ICard[5];
            hand[0] = new Card(Suit.Diamonds, CardValue.Four);
            hand[1] = new OptimizedCard(Suit.Clubs, CardValue.Ace);
            hand[2] = new Card(Suit.Spades, CardValue.Queen);
            hand[3] = new OptimizedCard(Suit.Hearts, CardValue.Nine);
            hand[4] = new OptimizedCard(Suit.Diamonds, CardValue.Four);


            Console.WriteLine("In my hand I have:");
            foreach (var card in hand)
            {
                Console.WriteLine(card);
            }

            for (var i = 0; i < hand.Length - 1; i++)
            {
                Console.WriteLine($"{hand[i]} is {(hand[i].CompareTo(hand[i + 1]) > 0 ? "greater" : "lesser")} than {hand[i + 1]}");
            }
        }
    }
}
