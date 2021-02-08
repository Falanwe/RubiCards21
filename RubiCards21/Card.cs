using System;
using System.Collections.Generic;
using System.Text;

namespace RubiCards21
{
    class Card
    {
        public CardColor color;
        public int value;

        public Card(CardColor color, int value)
        {
            this.color = color;
            this.value = value;
        }

        public override string ToString()
        {
            return HighValuesTranslator() + " of " + color.ToString();
        }

        public string HighValuesTranslator()
        {
            switch (value)
            {
                default: return value.ToString();
                case 11: return "Jack";
                case 12: return "Queen";
                case 13: return "King";
                case 14: return "Ace";
            }
        }

        public Card CompareTo(Card comparedCard)
        {
            if (comparedCard.value > value)
            {
                return comparedCard;
            }
            else if (comparedCard.value == value)
            {
                if ((int)comparedCard.color > (int)color)
                {
                    return comparedCard;
                }
                else return this;
            }
            else
            {
                return this;
            }
        }
    }
}
