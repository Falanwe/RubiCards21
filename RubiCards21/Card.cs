using System;

namespace Rubikards21
{
	public class Card : object, IComparable<Card>
	{
		public uint Value { get; private set; } = 0;
		public uint Symbol { get; private set; } = 0;

		public string[] Symbols { get; private set; } = DefaultSymbols;
		public string[] Values { get; private set; } = DefaultValues;

		public readonly static string[] DefaultSymbols = new string[] { "♠", "♥", "♦", "♣" };
		public readonly static string[] DefaultValues = new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "R", "A" };

		public Card(uint value, uint symbol)
		{
			Value = value;
			Symbol = symbol;
		}

		public Card(uint value, uint symbol, string[] symbols, string[] values) : this(value, symbol)
		{
			Symbols = symbols;
			Values = values;
		}

		public int CompareTo(Card other)
		{
			if (other == null) return 1;
			if (Value == other.Value) return Symbol.CompareTo(other.Symbol);
			return Value.CompareTo(other.Value);
		}

		public override string ToString()
		{
			return "|" + Symbols[Symbol] + Values[Value] + "|";
		}

		public static Card NewRandomCard() => NewRandomCard(DefaultSymbols, DefaultValues);

		public static Card NewRandomCard(string[] symbols, string[] values)
		{
			var random = new Random();
			uint randomValue = (uint)random.Next(values.Length - 1);
			uint randomSymbol = (uint)random.Next(symbols.Length - 1);
			return new Card(randomSymbol, randomValue, symbols, values);
		}

		public static Card[] NewDeck() => NewDeck(DefaultSymbols, DefaultValues);

		public static Card[] NewDeck(string[] symbols, string[] values)
		{
			var symbolsLength = symbols.Length;
			var valuesLength = values.Length;
			var deck = new Card[symbolsLength * valuesLength];
			for (uint s = 0; s < symbolsLength; s++)
			{
				for (uint v = 0; v < valuesLength; v++)
				{
					deck[s * valuesLength + v] = new Card(v, s, symbols, values);
				}
			}
			return deck;
		}
	}
}
