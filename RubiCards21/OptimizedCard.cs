using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace RubiCards21
{
    public class OptimizedCard : IComparable<ICard>, ICard
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

        private static byte? GetUnderlyingValue(ICard card)
        {
            switch (card)
            {
                case OptimizedCard optimized:
                    return optimized._value;
                default:
                    return (byte)(4 * (int)card?.Value + (int)card?.Suit);
            }
        }            

        public int CompareTo([AllowNull] ICard other) => _value.CompareTo(GetUnderlyingValue(other) ?? 0);
    }
}
