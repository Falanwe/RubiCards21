using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace RubiCards21
{
    public class Card : IComparable<ICard>, ICard
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
    }
}
