﻿using System;

namespace RubiCards21
{
    public class Card : IComparable<Card>
    {
        public CardSuit Suit { get; }
        public CardValue Value { get; }

        public Card(CardSuit suit, CardValue value)
        {
            this.Suit = suit;
            this.Value = value;
        }

        public override string ToString()
        {
            return this.Value + " of " + this.Suit;
        }

        public int CompareTo(Card other)
        {
            if (other.Value == this.Value)
            {
                if (other.Suit == suit) return 0;

                if (other.Suit > suit) return -1;
                
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
            return  (int)this.Suit*100 + (int)this.Value;
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