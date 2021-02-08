using System;

namespace RubiCards21
{
	/// <summary>
	/// Standard deck of 52 cards.
	/// </summary>
	/// <remarks>
	/// 13 Clubs, 13 Spades, 13 Hearts, 13 Diamonds
	/// </remarks>
	public class StandardDeck : Deck
	{
		public override string[] Suits => new string[] { "♠", "♥", "♦", "♣" };
		public override string[] Values => new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "R", "A" };

		public override Card NewCard(byte value, byte suit) => new StandardCard(value, suit);

		public StandardCard NewCard(StandardCardValue value, StandardCardSuit suit) => new StandardCard(value, suit);
	}
}
