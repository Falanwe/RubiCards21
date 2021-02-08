using System;
using System.Diagnostics.CodeAnalysis;

namespace RubiCards21
{
    public interface ICard: IComparable<ICard>, IEquatable<ICard>
    {
        Suit Suit { get; }
        CardValue Value { get; }
    }
}