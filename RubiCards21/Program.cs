using System;

namespace RubiCards21
{
    class Program
    {
        private static bool fastForward;
        
        static void Main(string[] args)
        {
            var deck = CreateDeck();
            Shuffle(deck);

            var halfLength = deck.Count / 2;
            
            var firstDeck = new List<Card>(halfLength);
            Shuffle(firstDeck);
            
            var secondDeck = new List<Card>(halfLength);
            Shuffle(secondDeck);

            for (var i = 0; i < deck.Count; i++)
            {
                if (i % 2 == 0) firstDeck.Add(deck[i]);
                else secondDeck.Add(deck[i]);
            }
            
            ShowCommands();
            Console.WriteLine("The match between Jérome & Tom begins");

            var round = 0;
            var spoils = new List<Card>()
            {
                DrawFrom(firstDeck),
                DrawFrom(secondDeck)
            };
            
            while (firstDeck.Any() && secondDeck.Any())
            {
                Console.WriteLine($"Round {round} starts!");
                
                if (spoils[0] == spoils[1])
                {
                    Console.WriteLine($"{spoils[0]} is equal to {spoils[1]}, continuing round.");

                    spoils.Add(DrawFrom(firstDeck));
                    spoils.Add(DrawFrom(secondDeck));
                    
                    spoils.Insert(0, DrawFrom(firstDeck));
                    spoils.Insert(0, DrawFrom(secondDeck));
                    
                    continue;
                }
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
                
                spoils.Clear();
                spoils.Add(DrawFrom(firstDeck));
                spoils.Add(DrawFrom(secondDeck));
                
                round++;
            }
            
            Console.WriteLine("The match is over.");
            if (!secondDeck.Any()) Console.WriteLine("Jérome wins!");
            else Console.WriteLine("Tom wins!");
        }

        private static List<Card> CreateDeck()
        {
            var types = Enum.GetValues(typeof(Type)).Cast<Type>();
            var values = Enum.GetValues(typeof(Value)).Cast<Value>();

            var length = types.Count() * values.Count();
            var deck = new List<Card>(length);
            
            foreach (var type in types)
            {
                foreach (var value in values) deck.Add(new Card(type, value));
            }

            return deck;
        }
        private static void Shuffle(IList<Card> deck, int seed = 0)
        {
            var copy = new List<Card>(deck.Count);
            foreach (var card in deck) copy.Add(card);

            var rnd = new Random(seed == 0 ? DateTime.Now.Millisecond : seed);
            for (var i = 0; i < deck.Count; i++)
            {
                var j = rnd.Next(0, copy.Count);
                var card = copy[j];
                copy.RemoveAt(j);

                deck[i] = card;
            }
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
