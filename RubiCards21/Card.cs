using System;

namespace Rubikards21
{
    public enum Value
    {
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }
    public enum Type
    {
        Clubs,
        Hearts,
        Diamonds,
        Spades
    }
    
    public struct Card : IComparable<Card>
    {
        public Card(Type type, Value value)
        {
            Type = type;
            Value = value;
        }

        public Type Type { get; private set; }
        public Value Value { get; private set; }
        
        public int CompareTo(Card other)
        {
            if (Type == other.Type) return Value.CompareTo(other.Value);
            return Type.CompareTo(other.Type);
        }

        public static bool operator > (Card first, Card second) => first.CompareTo(second) > 0;
        public static bool operator < (Card first, Card second) => first.CompareTo(second) < 0;
        
        public static bool operator >= (Card first, Card second) => first.CompareTo(second) >= 0;
        public static bool operator <= (Card first, Card second) => first.CompareTo(second) <= 0;
        
        public static bool operator == (Card first, Card second) => first.CompareTo(second) == 0;
        public static bool operator != (Card first, Card second) => first.CompareTo(second) != 0;

        public override string ToString() => $"{Value} of {Type}";
    }    
}
