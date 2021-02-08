using System;
using System.Collections.Generic;

namespace RubiCards21
{
    public interface ISorter
    {
        IEnumerable<T> Sort<T>(IEnumerable<T> list) where T : IComparable<T>;
    }
}