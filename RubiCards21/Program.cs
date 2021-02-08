using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bataille
{
    class Program
    {
        static void Main(string[] args)
        {
            //BattleGame game = new BattleGame();

            /*while (!game.IsFinished)
            {
                game.Update();
                game.Display();
            }*/

            Card[] cards = CardUtilities.RandomSet(1_000_000).ToArray();
            CardUtilities.Sort(ref cards, 0, cards.Length - 1);

            Card previous = null;
            foreach (var current in cards)
            {
                if (current.CompareTo(previous) < 0)
                {
                    throw new InvalidOperationException("Something went wrong in the sorting!");
                }
                previous = current;
            }

            Console.WriteLine($"{Card.compareCount} comparaisons");

            Console.ReadKey();
        }
    }
}
