using System;

namespace RubiCards21
{
	public static class CardUtility
	{
		public readonly static string[] DefaultSuit = new string[] { "♠", "♥", "♦", "♣" };
		public readonly static string[] DefaultValues = new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "R", "A" };

		public static Card NewCard(CardValue cardValue, CardSymbol cardSymbol)
		{
			return new Card((byte)cardValue, (byte)cardSymbol);
		}

		public static Card NewRandomCard() => NewRandomCard(DefaultSuit, DefaultValues);

		public static Card NewRandomCard(string[] symbols, string[] values)
		{
			var random = new Random();
			byte randomValue = (byte)random.Next(values.Length - 1);
			byte randomSymbol = (byte)random.Next(symbols.Length - 1);
			return new Card(randomSymbol, randomValue, symbols, values);
		}

		public static Card[] NewDeck() => NewDeck(DefaultSuit, DefaultValues);

		public static Card[] NewDeck(string[] symbols, string[] values)
		{
			var symbolsLength = symbols.Length;
			var valuesLength = values.Length;
			var deck = new Card[symbolsLength * valuesLength];
			for (byte s = 0; s < symbolsLength; s++)
			{
				for (byte v = 0; v < valuesLength; v++)
				{
					deck[s * valuesLength + v] = new Card(v, s, symbols, values);
				}
			}
			return deck;
		}
	}
}
