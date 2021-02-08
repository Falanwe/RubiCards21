using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RubiCards21
{
    public class CardStrengthSorter : ISorter
    {
        public IEnumerable<T> Sort<T>(IEnumerable<T> list) where T : IComparable<T>
        {
            return list.OrderBy(t => t);
        }
    }
}