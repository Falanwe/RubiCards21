using RubiCards21.Sorters;
using System;
using System.Diagnostics;
using System.Linq;

namespace RubiCards21
{
    class Program
    {
        private static readonly Stopwatch _stopwatch = new Stopwatch();
        private static void TestSorter(ICard[] randomSet, ISorter sorter)
        {
            _stopwatch.Restart();
            var sorted = sorter.Sort(randomSet).ToArray();
            Console.WriteLine($"With {sorter} I took {CardUtilities.ComparisonCount:N0} comparison to sort the set. It took {_stopwatch.ElapsedMilliseconds} ms");

            ICard previous = null;
            foreach (var current in sorted)
            {
                if (current.CompareTo(previous) < 0)
                {
                    throw new InvalidOperationException("Something went wrong in the sorting!");
                }
                previous = current;
            }
            Console.WriteLine();
            CardUtilities.ResetComparisionCount();
        }

        static void Main(string[] args)
        {
            var randomSet = CardUtilities.RandomSet(10_000).ToArray();

            TestSorter(randomSet, new SelectionSorter());
            TestSorter(randomSet, new SelectionSorterWithLinkedList());
            TestSorter(randomSet, new SelectionSorterWithArray());
            TestSorter(randomSet, new InsertionSorter());
            TestSorter(randomSet, new BestSorter());
            

            //sorter = new QuickSorter();
            //sorted = sorter.Sort(randomSet).ToArray();
            //Console.WriteLine($"With QuickSorter I took {CardUtilities.ComparisonCount:N0} comparison to sort the set.");

            //foreach(var c in randomSet
            //    .Take(10)
            //    .OrderBy(c => c.Suit)
            //    .ThenBy(c=> c.Value))
            //{
            //    Console.WriteLine(c);
            //}

            //previous = null;
            //foreach (var current in sorted)
            //{
            //    if (current.CompareTo(previous) < 0)
            //    {
            //        throw new InvalidOperationException("Something went wrong in the sorting!");
            //    }
            //    previous = current;
            //}
            //Console.WriteLine($"The set looks ordered.");
            //Console.WriteLine();

        }
    }
}
