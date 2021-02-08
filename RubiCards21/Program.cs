using System;

namespace RubiCards21
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Card cardA = UserCreateCard();
            Card cardB = UserCreateCard();
            Console.WriteLine(GetGreatest(cardA, cardB).ToString());
        }

        public static Card GetGreatest(Card cardA, Card cardB)
        {
            return cardA > cardB ? cardA : cardB;
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

            Console.WriteLine("[Created card : " + card.GetName() + "]");
            
            return card;
        }
    }
}