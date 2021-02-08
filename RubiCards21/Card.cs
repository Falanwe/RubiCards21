using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace RubiCards21
{
    public class Card : ICard
    {
        public Card(Suit suit, CardValue value)
        {
            Suit = suit;
            Value = value;
        }

        public Suit Suit { get; }

        public CardValue Value { get; }

        public int CompareTo([AllowNull] ICard other)
        {
            CardUtilities.CountComparison();

            if (other == null)
            {
                return 1;
            }

            if (Value > other.Value)
            {
                return 1;
            }
            else if (Value < other.Value)
            {
                return -1;
            }
            else
            {
                if (Suit > other.Suit)
                {
                    return 1;
                }
                else if (Suit < other.Suit)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
        }

        public override string ToString()
        {
            return $"{Value} of {Suit}";
        }

        public bool Equals([AllowNull] ICard other) => other != null && Value == other.Value && Suit == other.Suit;

        public override bool Equals(object obj) => Equals(obj as ICard);

        public override int GetHashCode() => (int)Suit * 1500450271 + (int)Value;

        public static bool operator >(Card card1, ICard card2) => CardUtilities.IsGreater(card1, card2);
        public static bool operator <(Card card1, ICard card2) => CardUtilities.IsLesser(card1, card2);
        public static bool operator >=(Card card1, ICard card2) => !CardUtilities.IsLesser(card1, card2);
        public static bool operator <=(Card card1, ICard card2) => !CardUtilities.IsGreater(card1, card2);

        public static bool operator ==(Card card1, ICard card2) => CardUtilities.IsEqual(card1, card2);
        public static bool operator !=(Card card1, ICard card2) => !CardUtilities.IsEqual(card1, card2);
    }
}
