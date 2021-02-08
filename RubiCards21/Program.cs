using System;

namespace RubiCards21
{
    class Program
    {
        private static bool fastForward;
        
        static void Main(string[] args)
        {
            var deck = CreateDeck();

            var halfLength = deck.Count / 2;
            var firstDeck = new List<Card>(halfLength);
            var secondDeck = new List<Card>(halfLength);

            for (var i = 0; i < deck.Count; i++)
            {
                if (i % 2 == 0) firstDeck.Add(deck[i]);
                else secondDeck.Add(deck[i]);
            }
            
            ShowCommands();
            Console.WriteLine("The match between Jérome & Tom begins");

            while (firstDeck.Any() && secondDeck.Any())
            {
                var spoils = new Card[2]
                {
                    DrawFrom(firstDeck),
                    DrawFrom(secondDeck)
                };

                if (spoils[0] == spoils[1]) throw new InvalidDataException("There should not be two of the same cards!");
                else
                {
                    if (spoils[0] > spoils[1])
                    {
                        Console.WriteLine($"{spoils[0]} beats {spoils[1]}. Jérome wins the round");
                        GiveSpoilsTo(spoils, firstDeck);
                    }
                    else
                    {
                        Console.WriteLine($"{spoils[1]} beats {spoils[0]}. Tom wins the round");
                        GiveSpoilsTo(spoils, secondDeck);
                    }
                }
                
                Console.WriteLine($"Jérome has {firstDeck.Count} cards left & Tom has {secondDeck.Count} cards left.");
                Console.WriteLine();
                
                if (!fastForward) ReadCommand();
            }
        }

        private static List<Card> CreateDeck()
        {
            var types = Enum.GetValues(typeof(Type)).Cast<Type>();
            var values = Enum.GetValues(typeof(Value)).Cast<Value>();

            var length = types.Count() * values.Count();
            
            var deck = new List<Card>(length);
            var indices = new List<int>(length);
            for (var i = 0; i < length; i++)
            {
                deck.Add(default);
                indices.Add(i);
            }

            var rnd = new Random();
            foreach (var type in types)
            {
                foreach (var value in values)
                {
                    var i = rnd.Next(0, indices.Count);
                    var index = indices[i];
                    indices.RemoveAt(i);
                    
                    deck[index] = new Card(type, value);
                }
            }

            return deck;
        }

        private static Card DrawFrom(IList<Card> deck)
        {
            var card = deck.First();
            deck.RemoveAt(0);

            return card;
        }
        private static void GiveSpoilsTo(IEnumerable<Card> spoils, IList<Card> deck)
        {
            foreach (var spoil in spoils) deck.Add(spoil);
        }

        private static void ReadCommand()
        {
            var state = true;
            var line = Console.ReadLine();
            
            while (state)
            {
                switch (line)
                {
                    case "help":
                        ShowCommands();
                        line = Console.ReadLine();
                        break;
                    
                    case "next":
                        state = false;
                        Console.WriteLine();
                        break;
                    
                    case "skip":
                        fastForward = true;
                        state = false;
                        Console.WriteLine();
                        break;
                }
            }
        }
        
        private static void ShowCommands()
        {
            Console.WriteLine("---| Commands :");
            Console.WriteLine("------> help : shows commands");
            Console.WriteLine("------> next : goes to next round");
            Console.WriteLine("------> skip : fast forward through all rounds");
            Console.WriteLine();
        }
    }
}
