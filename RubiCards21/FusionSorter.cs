using RubiCards21;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardBattle
{
    class FusionSorter : ISorter
    {
        public IEnumerable<T> Sort<T>(IEnumerable<T> setToSort) where T : IComparable<T>
        {
            return MergeSort(setToSort.ToArray());
        }

        public IEnumerable<T> MergeSort<T>(T[] l) where T : IComparable<T>
        {
            if (l.Length == 1) return l;
            T[] lf = GetLeft(l);
            T[] rg = GetRight(l);
            Console.WriteLine("----------------");
            for (int i = 0; i < lf.Length; i++)
            {
                Console.WriteLine(lf[i]);
            }
            Console.WriteLine("--- " + lf.Length + " " + rg.Length);
            for (int i = 0; i < rg.Length; i++)
            {
                Console.WriteLine(rg[i]);
            }
            return Merge(MergeSort(GetLeft(l)).ToArray(), MergeSort(GetRight(l)).ToArray());
        }

        public T[] Merge<T>(T[] l1, T[] l2) where T : IComparable<T>
        {
            int i = 0, j = 0;
            T[] m = new T[l1.Length + l2.Length];
            while (i < l1.Length && j < l2.Length)
            {
                if (l1[i].CompareTo(l2[j]) < 0)
                {
                    m[i + j] = l1[i++];
                }
                else if (l1[i].CompareTo(l2[j]) > 0)
                {
                    m[i + j] = l2[j++];
                } else
                {
                    m[i + j] = l1[i++];
                    m[i + j] = l2[j++];
                }
            }
            return m;
        }

        public T[] GetLeft<T>(T[] l)
        {
            int i = 0;
            T[] t = new T[l.Length / 2];
            while(i < (l.Length / 2))
            {
                t[i] = l[i++]; 
            }
            return t;
        }

        public T[] GetRight<T>(T[] l)
        {
            int i = 0;
            int j = (l.Length / 2);
            T[] t = new T[l.Length - (l.Length / 2)];
            while (j < l.Length)
            {
                t[i++] = l[j++];
            }
            return t;
        }
    }
}
