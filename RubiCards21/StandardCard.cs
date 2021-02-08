namespace RubiCards21
{
	/// <summary>
	/// Standard card.
	/// </summary>
	/// <remarks>
	/// <para>Suit of Spades, Hearts, Diamonds, Clubs.</para>
	/// <para>Value of 2, 3, 4, 5, 6, 7, 8, 9, 10, J, Q, R, A</para>
	/// </remarks>
	public class StandardCard : Card
	{
		public StandardCardValue CardValue => (StandardCardValue)Value;
		public StandardCardSuit CardSuit => (StandardCardSuit)Suit;

		public StandardCard()
		{
		}

		public StandardCard(byte value, byte suit) : base(value, suit)
		{
		}

		public StandardCard(StandardCardValue value, StandardCardSuit suit) : base((byte)value, (byte)suit)
		{
		}

		public override string ToString() => CardValue + " of " + CardSuit;
	}
}
