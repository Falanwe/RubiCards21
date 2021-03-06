﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace RubiCards21.Sorters
{
    public class BestSorter : ISorter
    {
        public IEnumerable<T> Sort<T>(IEnumerable<T> setToSort) where T : class, IComparable<T>
        => setToSort.OrderBy(t => t);
    }
}
