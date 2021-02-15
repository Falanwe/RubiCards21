using System;
using System.Collections.Generic;
using System.Text;

namespace RubiCards21.Sorters
{
    public interface ISorter
    {
        IEnumerable<T> Sort<T>(IEnumerable<T> setToSort) where T : class, IComparable<T>;
    }
}
