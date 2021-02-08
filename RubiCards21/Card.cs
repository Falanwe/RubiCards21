using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace CardBattle
{
    class Card : IComparable<Card>
    {
        public string Name { get; set; } = "";
        public int Value { get; set; } = 0;

        public Card(string name, int value)
        {
            Name = name;
            Value = value;
        }

        public int CompareTo([AllowNull] Card other)
        {
            return Value - other.Value;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
