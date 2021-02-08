using System;

namespace RubiCards21
{
    public interface ICard: IComparable<ICard>
    {
        Suit Suit { get; }
        CardValue Value { get; }
    }
}