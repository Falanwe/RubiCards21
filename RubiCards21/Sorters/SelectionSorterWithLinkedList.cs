using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RubiCards21.Sorters
{
    public class SelectionSorterWithLinkedList : ISorter
    {
        public IEnumerable<T> Sort<T>(IEnumerable<T> setToSort) where T : IComparable<T>
        {
            var copy = new LinkedList<T>(setToSort);

            while (copy.Any())
            {
                var current = copy.Min();

                yield return current;
                copy.Remove(current);
            }
        }
    }
}
