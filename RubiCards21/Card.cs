using System;

namespace Hura
{
    public class Card : IComparable<Card>
    {
        public CardSuit Color;
        public CardValue Value;

        public Card(CardSuit color, CardValue value)
        {
            this.Color = color;
            this.Value = value;
        }

        public string GetName()
        {
            return this.Value + " of " + this.Color;
        }

        public int CompareTo(Card other)
        {
            if (other.Value == this.Value)
            {
                if (other.Color == this.Color) return 0;

                if (other.Color > this.Color) return -1;
                
                return 1;
            }

            if (other.Value > this.Value) return -1;

            return 1;
        }

        #region Operators

        public static bool operator >(Card cardA, Card cardB)
        {
            return cardA.CompareTo(cardB) == 1;
        }

        public static bool operator <(Card cardA, Card cardB)
        {
            return cardA.CompareTo(cardB) == -1;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || ! this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Card otherCard = (Card) obj;
                return otherCard.CompareTo(this) == 0;
            }
        }

        public override int GetHashCode()
        {
            return  (int)this.Color*100 + (int)this.Value;
        }
        
        public static bool operator ==(Card cardA, Card cardB)
        {
            return cardA.CompareTo(cardB) == 0;
        }

        public static bool operator !=(Card cardA, Card cardB)
        {
            return !(cardA == cardB);
        }
        
        public static bool operator >=(Card cardA, Card cardB)
        {
            return cardA.CompareTo(cardB) >= 0;
        }

        public static bool operator <=(Card cardA, Card cardB)
        {
            return cardA.CompareTo(cardB) <= 0;
        }
        
        #endregion
    }

    public enum CardSuit
    {
        Cubs,
        Diamonds,
        Hearts,
        Spades
    }

    public enum CardValue
    {
        Two = 2,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Height,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }
}