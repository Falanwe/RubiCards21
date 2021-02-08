using System;

namespace RubiCards21
{
    public static class CardUtility
    {
        public static CardSuit[] GetSuits() => (CardSuit[])Enum.GetValues(typeof(CardSuit));
        public static CardValue[] GetValues() => (CardValue[])Enum.GetValues(typeof(CardValue));
        public static CardSuit GetRandomSuit() => CardGameUtility.RandomEnum<CardSuit>();
        public static CardValue GetRandomValue() => CardGameUtility.RandomEnum<CardValue>();
        public static Card GetGreatest(Card cardA, Card cardB) => cardA > cardB ? cardA : cardB;
    }
}