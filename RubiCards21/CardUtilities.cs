using System;
using System.Collections.Generic;

namespace RubiCards21
{
	public static class CardUtilities
	{
		private static readonly Random random = new Random();

		public static T ToDeck<T>(this IEnumerable<Card> cards) where T : Deck
		{
			var deck = Activator.CreateInstance<T>();
			foreach (var item in cards)
			{
				deck.Add(item);
			}
			return deck;
		}

		public static Card NewRandomCard<T>(this Deck deck) where T : Card
		{
			lock (random)
			{
				byte randomValue = (byte)random.Next(deck.Values.Length - 1);
				byte randomSuit = (byte)random.Next(deck.Suits.Length - 1);
				return deck.NewCard(randomValue, randomSuit);
			}
		}
	}
}
