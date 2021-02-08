using System;

namespace RubiCards21
{
	public class Card : IComparable<Card>
	{
		public byte Value { get; } = 0;
		public byte Suit { get; } = 0;

		public string[] Values { get; } = new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "R", "A" };
		public string[] Suits { get; } = new string[] { "♠", "♥", "♦", "♣" };

		public Card(byte value, byte suit)
		{
			Value = value;
			Suit = suit;
		}

		public Card(byte value, byte suit, string[] symbols, string[] values) : this(value, suit)
		{
			Suits = symbols;
			Values = values;
		}

		public int CompareTo(Card other)
		{
			if (other == null) return 1;
			if (Value == other.Value) return Suit.CompareTo(other.Suit);
			return Value.CompareTo(other.Value);
		}

		public override string ToString()
		{
			return "|" + Suits[Suit] + Values[Value] + "|";
		}
	}
}
