using System;
using System.Diagnostics.CodeAnalysis;

namespace RubiCards21
{
	/// <summary>
	/// Simple game card
	/// </summary>
	public abstract class Card : IComparable<Card>
	{
		public byte Value { get; } = 0;
		public byte Suit { get; } = 0;

		public Card()
		{
		}

		public Card(byte value, byte suit)
		{
			this.Value = value;
			this.Suit = suit;
		}

		public override string ToString() => Value + " of " + Suit;

		public int CompareTo([AllowNull] Card other)
		{
			if (other == null) return 1;
			if (Value == other.Value) return Suit.CompareTo(other.Suit);
			return Value.CompareTo(other.Value);
		}

		public static bool operator <(Card left, Card right)
		{
			return left.CompareTo(right) < 0;
		}

		public static bool operator <=(Card left, Card right)
		{
			return left.CompareTo(right) <= 0;
		}

		public static bool operator >(Card left, Card right)
		{
			return left.CompareTo(right) > 0;
		}

		public static bool operator >=(Card left, Card right)
		{
			return left.CompareTo(right) >= 0;
		}
	}
}
