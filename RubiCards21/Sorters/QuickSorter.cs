using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace RubiCards21.Sorters
{
    public class QuickSorter : ISorter
    {
        public IEnumerable<T> Sort<T>(IEnumerable<T> setToSort) where T : class, IComparable<T>
        {
            if (!setToSort.Skip(1).Any())
            {
                return setToSort;
            }

            var groups = setToSort
                .Select((e, index) => (e, index))
                .GroupBy(pair => pair.index % 2, pair => pair.e);

            var s1 = Sort(groups.First());
            var s2 = Sort(groups.Last());

            var enumerator1 = s1.GetEnumerator();
            enumerator1.MoveNext();

            var enumerator2 = s2.GetEnumerator();
            enumerator2.MoveNext();

            IEnumerable<T> FinishEnumerator(IEnumerator<T> enumerator)
            {
                yield return enumerator.Current;
                while (enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                }
            }

            IEnumerable<T> Fuse()
            {
                while (true)
                {
                    if (enumerator1.Current.CompareTo(enumerator2.Current) < 0)
                    {
                        yield return enumerator1.Current;
                        if (!enumerator1.MoveNext())
                        {
                            foreach (var e in FinishEnumerator(enumerator2))
                            {
                                yield return e;
                            }
                            yield break;
                        }
                    }
                    else
                    {
                        yield return enumerator2.Current;
                        if (!enumerator2.MoveNext())
                        {
                            foreach (var e in FinishEnumerator(enumerator1))
                            {
                                yield return e;
                            }
                            yield break;
                        }
                    }
                }
            }

            return Fuse();
        }
    }
}

