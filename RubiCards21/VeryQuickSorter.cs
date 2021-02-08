using System;
using System.Collections.Generic;
using System.Linq;

namespace RubiCards21
{
    public class VeryQuickSorter : ISorter
    {
        private static ulong comparisons = 0;

        public IEnumerable<T> Sort<T>(IEnumerable<T> setToSort) where T : IComparable<T>
        {
            var array = setToSort.ToArray();
            Sort(array, 0, array.Length - 1);

            Console.WriteLine($"I counted {comparisons} comparisons");

            return array;
        }

        private void Sort<T>(T[] array, int l, int r) where T : IComparable<T>
        {
            int i, j;
            T x;

            i = l;
            j = r;

            x = array[(l + r) / 2];
            while (true)
            {
                while (true)
                {
                    comparisons++;
                    if (array[i].CompareTo(x) >= 0)
                    {
                        break;
                    }
                    i++;
                }
                while (true)
                {
                    comparisons++;
                    if ((x.CompareTo(array[j]) >= 0))
                    {
                        break;
                    }
                    j--;
                }

                if (i <= j)
                {
                    var copy = array[j];
                    array[j] = array[i];
                    array[i] = copy;

                    i++;
                    j--;
                }

                if (i > j) break;
            }

            if (l < j) Sort(array, l, j);
            if (i < r) Sort(array, i, r);
        }
    }
}