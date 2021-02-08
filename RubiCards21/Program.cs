using System;
using System.Linq;

namespace RubiCards21
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            CardHand hand = new CardHand(10);
            ISorter sorter = new CardStrengthSorter();
            hand.ResetHand(sorter.Sort(hand.Cards).ToArray());
            foreach (var card in hand.Cards)
            {
                Console.WriteLine(card.ToString());
            }
            return;
        }

        public static Card UserCreateCard()
        {
            Console.WriteLine("Enter card color : ");
            string userInput = Console.ReadLine();
            CardSuit cardColor;
            
            while (!Enum.TryParse(userInput, true, out cardColor))
            {
                Console.WriteLine("Invalid color, accepted colors : ");
                
                foreach (CardSuit color in Enum.GetValues(typeof(CardSuit)))
                {
                    Console.WriteLine("- " + color);
                }
                    
                Console.WriteLine("Enter color :");

                userInput = Console.ReadLine();
            }

            Console.WriteLine("Enter card value : ");
            userInput = Console.ReadLine();
            CardValue cardValue;

            while (!Enum.TryParse(userInput, true, out cardValue))
            {
                Console.WriteLine("Invalid value, accepted values : ");
                
                foreach (CardValue color in Enum.GetValues(typeof(CardValue)))
                {
                    Console.WriteLine("- " + color);
                }
                    
                Console.WriteLine("Enter value :");

                userInput = Console.ReadLine();
            }
            
            Card card = new Card(cardColor, cardValue);

            Console.WriteLine("[Created card : " + card.ToString() + "]");
            
            return card;
        }
    }
}