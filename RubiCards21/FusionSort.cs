using RubiCards21.Sorters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RubiCards21
{
    public class FusionSort : ISorter
    {
        public IEnumerable<T> Sort<T>(IEnumerable<T> setToSort) where T : class, IComparable<T>
        {
            if(!setToSort.Any())
            {
                return setToSort;
            }

            var pivot = setToSort.First();
            var groups = setToSort.Skip(1).GroupBy(item => Math.Sign(item.CompareTo(pivot)));

            var smaller = groups.FirstOrDefault(g => g.Key <0) ?? Enumerable.Empty<T>();
            var bigger = groups.FirstOrDefault(g => g.Key >0) ?? Enumerable.Empty<T>();
            var equals = groups.FirstOrDefault(g => g.Key == 0) ?? Enumerable.Empty<T>();

            return Sort(smaller).Append(pivot).Concat(equals).Concat(Sort(bigger));
        }
    }
}
