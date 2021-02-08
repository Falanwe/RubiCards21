using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ExoCSharp1
{
    public enum CardColor { Trefle, Carreau, Coeur, Pique }

    class Card : IComparable<Card>
    {
        public int value { get; }
        public CardColor color { get; }

        //private string[] printSymbols = new string[] { "♠", "♥", "♦", "♣" };
        private string[] printValues = new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "R", "A" };

        public Card(int newValue, CardColor newColor)
        {
            if(newValue < 0)
            {
                value = 0;
            }
            else if(newValue > 11)
            {
                value = 11;
            }
            else
            {
                value = newValue;
            }

            color = newColor;
        }

        public override string ToString()
        {
            return " " + printValues[value] + " " + color;
        }

        public int CompareTo([AllowNull] Card other)
        {
            if(other != null)
            {
                if(value == other.value)
                {
                    return CompareColor(color, other.color);
                }
                else
                {
                    return value - other.value;
                }
            }
            else
            {
                return 1;
            }
        }

        private int CompareColor(CardColor baseColor, CardColor otherColor)
        {
            if(baseColor > otherColor)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
    }
}
