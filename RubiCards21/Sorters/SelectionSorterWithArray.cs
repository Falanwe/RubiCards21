using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RubiCards21.Sorters
{
    public class SelectionSorterWithArray : ISorter
    {
        public IEnumerable<T> Sort<T>(IEnumerable<T> setToSort) where T : IComparable<T>
        {
            var copy = setToSort.Select(item => (item, isSet: true)).ToArray();

            var count = 0;
            while (count != copy.Length)
            {
                var set = false;
                T value = default(T);
                var index = 0;
                for (var i = 0; i<copy.Length; i++)
                {
                    if(copy[i].Item2 &&(!set || value.CompareTo(copy[i].item) > 0))
                    {
                        set = true;
                        value = copy[i].item;
                        index = i;
                    }
                }
                yield return value;
                copy[index] = (default(T), false);
                count++;
            }
        }
    }
}
