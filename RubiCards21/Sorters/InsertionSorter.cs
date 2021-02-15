using System;
using System.Collections.Generic;
using System.Text;

namespace RubiCards21.Sorters
{
    public class InsertionSorter : ISorter
    {
        public IEnumerable<T> Sort<T>(IEnumerable<T> setToSort) where T : IComparable<T>
        {
            var result = new LinkedList<T>();
            
            foreach(var item in setToSort)
            {
                var node = result.First;
                while(node != null && item.CompareTo(node.Value) > 0)
                {
                    node = node.Next;
                }
                if (node == null)
                {
                    result.AddLast(item);
                }
                else
                {
                    result.AddBefore(node, item);
                }
            }

            return result;
        }
    }
}
