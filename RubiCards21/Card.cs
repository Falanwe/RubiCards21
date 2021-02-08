using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bataille
{
    class Card : IComparable<Card>
    {
        public CardValue Value
        {
            get;
            private set;
        }

        public CardColor Color
        {
            get;
            private set;
        }

        public Card(CardValue value, CardColor color)
        {
            Value = value;
            Color = color;
        }

        public int CompareTo(Card other)
        {
            return Value.CompareTo(other.Value);
        }

        public override string ToString()
        {
            return $"{Value} {Color}";
        }
        #region Operators
        public static bool operator <(Card c1, Card c2)
        {
            return c1.CompareTo(c2) < 0;
        }

        public static bool operator >(Card c1, Card c2)
        {
            return c1.CompareTo(c2) > 0;
        }

        public static bool operator >=(Card c1, Card c2)
        {
            return c1.CompareTo(c2) >= 0;
        }

        public static bool operator <=(Card c1, Card c2)
        {
            return c1.CompareTo(c2) <= 0;
        }
        #endregion
    }
}
