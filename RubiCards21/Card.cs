using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bataille
{
    class Card : IComparable<Card>
    {
        public static int compareCount = 0;
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
            compareCount++;
            if (other == null) return 1;
            return Value.CompareTo(other.Value);
        }
        #region override
        public override string ToString()
        {
            return $"{Value} of {Color}";
        }

        public override bool Equals(object obj) => Equals(obj as Card);

        public override int GetHashCode() => (int)Color * 1500450271 + (int)Value;
        #endregion        
        
        #region Operators
        public static bool operator <(Card c1, Card c2)
        {
            if (c1 == null) return c2 != null;
            return c1.CompareTo(c2) < 0;
        }

        public static bool operator >(Card c1, Card c2)
        {
            if (c1 == null) return false;
            return c1.CompareTo(c2) > 0;
        }

        public static bool operator >=(Card c1, Card c2)
        {
            return !(c1.CompareTo(c2) < 0);
        }

        public static bool operator <=(Card c1, Card c2)
        {
            return !(c1.CompareTo(c2) > 0);
        }

        public static bool operator ==(Card c1, Card c2)
        {
            if(ReferenceEquals(c1,null))
            {
                return ReferenceEquals(c2, null);
            }
            else
            {
                return c1.Equals(c2);
            }
        }
        public static bool operator !=(Card c1, Card c2)
        {
            return !(c1 == c2);
        }
        #endregion

        private bool Equals(Card other)
        {
            return other != null && Value == other.Value && Color == other.Color;
        }
    }
}
