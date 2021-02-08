using System;
using System.Collections.Generic;
using System.Text;

namespace RubiCards21
{
    public class Card
    {
        public Card(Suit suit, CardValue value)
        {
            Suit = suit;
            Value = value;
        }

        public Suit Suit { get; }

        public CardValue Value { get;}

        public override string ToString()
        {
            return $"{Value} of {Suit}";
        }
    }
}
