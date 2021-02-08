using System.Collections.Generic;
using System.Text;

namespace RubiCards21
{
	public abstract class Deck : List<Card>
	{
		public virtual string[] Suits { get; } = new string[0];
		public virtual string[] Values { get; } = new string[0];

		public Deck()
		{
		}

		public Deck(string[] suits, string[] values) : this()
		{
			Suits = suits;
			Values = values;
		}

		public abstract Card NewCard(byte value, byte suit);

		/// <summary>
		/// Add default cards implemented in the deck
		/// </summary>
		public void AddDefaultCards()
		{
			var symbolsLength = Suits.Length;
			var valuesLength = Values.Length;

			for (byte s = 0; s < symbolsLength; s++)
			{
				for (byte v = 0; v < valuesLength; v++)
				{
					Add(NewCard(v, s));
				}
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			var iterator = GetEnumerator();
			while (iterator.MoveNext())
			{
				var card = iterator.Current;
				var suit = card.Suit;
				var value = card.Value;
				if (suit < 0 || suit >= Suits.Length || value < 0 || value >= Values.Length)
				{
					stringBuilder.AppendLine(card.ToString());
				}
				else
				{
					stringBuilder.Append(Suits[suit]);
					stringBuilder.Append(Values[value]);
					stringBuilder.Append(" ");
				}
			}
			return stringBuilder.ToString();
		}
	}
}
