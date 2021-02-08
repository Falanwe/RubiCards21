using System;
using System.Collections.Generic;
using System.Text;

namespace RubiCards21
{
    public class OptimizedCard
    {
        public OptimizedCard(Suit suit, CardValue value)
        {
            _value = (byte)(4 * (int)value + (int)suit);
        }

        private readonly byte _value;
        public Suit Suit => (Suit)(_value % 4);
        public CardValue Value => (CardValue)(_value / 4);

        public override string ToString()
        {
            return $"{Value} of {Suit}";
        }
    }
}
