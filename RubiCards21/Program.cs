using System;
using System.Linq;

namespace RubiCards21
{
    class Program
    {
        static void Main(string[] args)
        {
            var randomSet = CardUtilities.RandomSet(1_000_000).ToArray();
            var sorter = new BestSorter();

            var sorted = sorter.Sort(randomSet).ToArray();

            Console.WriteLine($"I took {CardUtilities.ComparisonCount:N0} comparison to sort the set.");

            ICard previous = null;
            foreach (var current in sorted)
            {
                if (current.CompareTo(previous) < 0)
                {
                    throw new InvalidOperationException("Something went wrong in the sorting!");
                }
                previous = current;
            }

            Console.WriteLine($"The set looks ordered.");
        }
    }
}
