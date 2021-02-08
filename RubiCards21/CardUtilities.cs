using System;
using System.Collections.Generic;
using System.Text;

namespace RubiCards21
{
    public static class CardUtilities
    {
        private static readonly Random _rand = new Random();

        public static ICard RandomCard()
        {
            lock(_rand)
            {
                var suit = (Suit)_rand.Next(4);
                var value = (CardValue)_rand.Next((int)CardValue.Two, (int)CardValue.Ace + 1);
                if (_rand.Next() %2 == 0)
                {
                    return new Card(suit, value);
                }
                else
                {
                    return new OptimizedCard(suit, value);
                }
            }
        }

        public static bool IsGreater(ICard card1, ICard card2)
        {
            if (card1 == null)
            {
                return false;
            }
            else
            {
                return card1.CompareTo(card2) > 0;
            }
        }

        public static bool IsLesser(ICard card1, ICard card2)
        {
            if (card1 == null)
            {
                return card2 != null;
            }
            else
            {
                return card1.CompareTo(card2) < 0;
            }
        }

        public static bool IsEqual(ICard card1, ICard card2)
        {
            if(card1 == null)
            {
                return card2 == null;
            }
            return card1.Equals(card2);
        }
    }
}
