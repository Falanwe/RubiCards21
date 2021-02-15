using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RubiCards21.Sorters
{
    class BubbleSorter : ISorter
    {
        public IEnumerable<T> Sort<T>(IEnumerable<T> setToSort) where T : class, IComparable<T>
        {
            var result = setToSort.ToArray();
            void Swap(int i, int j)
            {
                var intermediate = result[i];
                result[i] = result[j];
                result[j] = intermediate;
            }

            for (var i = 1; i < result.Length; )
            {
                var lastSwap = 0;
                for (var j = 0; j < result.Length - i; j++)
                {
                    if (result[j].CompareTo(result[j + 1]) > 0)
                    {
                        lastSwap = j;
                        Swap(j, j + 1);
                    }
                }
                i = result.Length-lastSwap;
            }
            return result;
        }
    }
}
