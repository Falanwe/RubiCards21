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
                    return (byte?)(4 * (int?)card?.Value + (int?)card?.Suit);
            }
        }

        public int CompareTo([AllowNull] ICard other)
        {
            CardUtilities.CountComparison();
            return _value.CompareTo(GetUnderlyingValue(other) ?? 0);
        }

        public bool Equals([AllowNull] ICard other) => _value == GetUnderlyingValue(other);

        public override bool Equals(object obj) => Equals(obj as ICard);
        public override int GetHashCode() => (int)Suit * 1500450271 + (int)Value;

        public static bool operator >(OptimizedCard card1, ICard card2) => CardUtilities.IsGreater(card1, card2);
        public static bool operator <(OptimizedCard card1, ICard card2) => CardUtilities.IsLesser(card1, card2);
        public static bool operator >=(OptimizedCard card1, ICard card2) => !CardUtilities.IsLesser(card1, card2);
        public static bool operator <=(OptimizedCard card1, ICard card2) => !CardUtilities.IsGreater(card1, card2);

        public static bool operator ==(OptimizedCard card1, ICard card2) => CardUtilities.IsEqual(card1, card2);
        public static bool operator !=(OptimizedCard card1, ICard card2) => !CardUtilities.IsEqual(card1, card2);
    }
}
