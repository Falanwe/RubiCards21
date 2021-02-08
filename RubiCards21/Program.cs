using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;

namespace RubiCards21
{
    class Program
    {
        static List<Card> allCards = new List<Card>();

        static List<Card> deckA = new List<Card>();
        static List<Card> deckB = new List<Card>();

        static int gameTurns = 0;
        static int gamesSimulated = 0;
        static int shuffleEachHand = 0;
        static int shuffleType = 0;

        static string onlyAceOfSpadesInDeckA;

        static string simulationSeed;
        static string winnerSeed;

        static void Main(string[] args)
        {
            CreateCards(52, 4);


            Console.WriteLine("Simulations Count :");
            int.TryParse(Console.ReadLine(), out gamesSimulated);
            Console.WriteLine("DeckA : only Ace Of Spade ? Y / N");
            onlyAceOfSpadesInDeckA = Console.ReadLine();

            if (onlyAceOfSpadesInDeckA != "Y")
            {
                Console.WriteLine("1 of 2 card = 0 \nFull Random Shuffle = 1");
                int.TryParse(Console.ReadLine(), out shuffleType);
            }

            Console.WriteLine("Cards at the bottom of the deck = 0 \nShuffle Each Hand = 1");
            int.TryParse(Console.ReadLine(), out shuffleEachHand);


            for (int i = 0; i < gamesSimulated; i++)
            {
                ShuffleDecks();

                while (deckB.Count > 0 && deckA.Count > 0)
                {
                    Card a;
                    Card b;

                    if (shuffleEachHand == 1)
                    {
                        a = GetRandomCard(deckA);
                        b = GetRandomCard(deckB);
                    }
                    else
                    {
                        a = GetTopCard(deckA);
                        b = GetTopCard(deckB);
                    }


                    Console.WriteLine("A = " + a);
                    Console.WriteLine("B = " + b);

                    gameTurns++;
                    Winner(a, b);
                }

                if (deckA.Count == 0) Console.WriteLine("____B Won !");
                else Console.WriteLine("____A Won !");

                simulationSeed += (gameTurns + "_");
                winnerSeed += (deckA.Count == 0 ? "B_" : "A_");
                gameTurns = 0;

                Console.WriteLine("\n");

            }

            using (StreamWriter sw = File.CreateText(PathName()))
            {
                sw.WriteLine(simulationSeed);
            }

            using (StreamWriter sw = File.CreateText("Winner_" + gamesSimulated + ".txt"))
            {
                sw.WriteLine(winnerSeed);
            }

        }

        static void CreateCards(int deckSize, int colorCount)
        {
            for (int i = 0; i < colorCount; i++) //Flemme d'avoir un flatten array ahahah
            {
                for (int j = 2; j < (int)(deckSize / colorCount) + 2; j++)
                {
                    allCards.Add(new Card((CardColor)i, j));
                    //Console.WriteLine(new Card((CardColor)i, j));
                }
            }
        }

        static void ShuffleDecks()
        {
            List<Card> tempList = new List<Card>();

            deckA.Clear();
            deckB.Clear();

            foreach (Card c in allCards)
            {
                tempList.Add(c);
            }

            if (onlyAceOfSpadesInDeckA == "Y")
            {
                deckA.Add(tempList[tempList.Count - 1]);
                tempList.RemoveAt(tempList.Count - 1);
                deckB = tempList;
                return;
            }



            for (int i = 0; i < allCards.Count; i++)
            {
                Card temp;
                if (shuffleType == 1) temp = GetRandomCard(tempList);
                else temp = GetTopCard(tempList);

                if (i % 2 == 1)
                {
                    deckA.Add(temp);
                }
                else
                {
                    deckB.Add(temp);
                }

                tempList.Remove(temp);
            }
        }

        static void Winner(Card a, Card b)
        {

            if (shuffleEachHand == 1)
            {
                if (a.CompareTo(b) == a)
                {
                    deckA.Add(b);
                    deckB.Remove(b);
                }
                else
                {
                    deckB.Add(a);
                    deckA.Remove(a);
                }
            }
            else
            {
                if (a.CompareTo(b) == a)
                {
                    deckA.Add(a);
                    deckA.RemoveAt(0);
                    deckA.Add(b);
                    deckB.Remove(b);
                }
                else
                {
                    deckB.Add(b);
                    deckB.RemoveAt(0);
                    deckB.Add(a);
                    deckA.Remove(a);
                }
            }

            Console.WriteLine(deckA.Count);

            Console.WriteLine("Winner = " + a.CompareTo(b).ToString() + "\n");
        }

        static Card GetRandomCard(List<Card> from)
        {
            Random r = new Random();
            return from[r.Next(0, from.Count)];
        }

        static Card GetTopCard(List<Card> from)
        {
            return from[0];
        }

        static string PathName()
        {
            string pathName = "GamesDuration_";

            if (shuffleType == 0)
            {
                pathName += "1of2_";
            }
            else
            {
                pathName += "Random_";
            }

            if (shuffleEachHand == 0)
            {
                pathName += "BottomNoShuffle_";
            }
            else
            {
                pathName += "FullShuffle_";
            }

            if (onlyAceOfSpadesInDeckA == "Y")
            {
                pathName += "AceAlone_";
            }

            pathName += (gamesSimulated + ".txt");

            return pathName;
        }
    }
    public enum CardColor { Club, Diamond, Heart, Spade }
}
