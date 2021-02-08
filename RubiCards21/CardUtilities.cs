using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bataille
{
    class CardUtilities
    {
        private static readonly Random _rand = new Random();
        public static Card RandomCard()
        {
            lock (_rand)
            {
                CardColor suit = (CardColor)_rand.Next(4);
                CardValue value = (CardValue)_rand.Next((int)CardValue.Two, (int)CardValue.Ace + 1);               
                return new Card(value, suit);
            }
        }

        public static IEnumerable<Card> RandomSet(int count)
        {
            for (var i = 0; i < count; i++)
            {
                yield return RandomCard();
            }
        }

        public static void Sort(ref Card[] cards, int start, int last)
        {
            if (start < last)
            {
                int pivot = GetPivot(ref cards, start, last);                
                
                Sort(ref cards, start, pivot);                
                Sort(ref cards, pivot + 1, last);
            }
        }
        private static int GetPivot(ref Card[] cards, int start, int last)
        {
            Card pivot = cards[(start+last)/2];
            int i = start - 1;
            int j = last + 1;
            while(i < j)
            {
                do
                {
                    i++;
                } while (cards[i] < pivot);

                do
                {
                    j--;
                } while (cards[j] > pivot);
                if (i < j)
                {
                    Swap(ref cards[i], ref cards[j]);
                }                
            }
            return j;
        }

        private static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }
    }
}
