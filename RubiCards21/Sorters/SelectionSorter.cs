using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RubiCards21.Sorters
{
    public class SelectionSorter : ISorter
    {
        private static T Min<T>(IEnumerable<T> set) where T: IComparable<T>
        {
            if(set == null)
            {
                throw new ArgumentNullException();
            }
            if(!set.Any())
            {
                throw new ArgumentException();
            }

            var result = set.First();

            foreach(var item in set.Skip(1))
            {
                if(result.CompareTo(item) > 0)
                {
                    result = item;
                }
            }

            return result;
        }

        public IEnumerable<T> Sort<T>(IEnumerable<T> setToSort) where T : class, IComparable<T>
        {
            var copy = setToSort.ToList();

            while(copy.Any())
            {
                //var current = copy.Min();
                var current = Min(copy);

                yield return current;
                copy.Remove(current);
            }
        }
    }
}
