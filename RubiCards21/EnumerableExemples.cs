using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace RubiCards21
{
    public static class EnumerableExemples
    {
        public static void Enumerate<T>(IEnumerable<T> enumerable)
        {
            foreach (var t in enumerable)
            {
                Console.WriteLine(t);
            }
            // equivalent to the following:
            //{
            //    using var enumerator = enumerable.GetEnumerator();

            //    while (enumerator.MoveNext())
            //    {
            //        var t = enumerator.Current;
            //        Console.WriteLine(t);
            //    }
            //}


            Console.WriteLine("No elements left in the enumerable");

        }

        public static void DoThings()
        {
            var source = Enumerable.Range(0, 10_000);

            var count = 0;
            var filtered = source.Select(n =>
            {
                count++;
                return n * n;
            })
                .Select(n => n - 2)
                .Where(n => n % 2 == 0);

            Console.WriteLine(count);

            foreach (var i in filtered)
            {
            }

            Console.WriteLine(count);

            foreach (var i in filtered)
            {
            }

            Console.WriteLine(count);
        }

        public static IEnumerable<BigInteger> Fibonacci()
        {
            BigInteger previous = 0;
            BigInteger current = 1;

            yield return previous;
            yield return current;
            while(true)
            {
                var next = previous + current;
                yield return next;
                previous = current;
                current = next;
            }
        }
    }
}
