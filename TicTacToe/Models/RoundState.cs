using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Models
{
    public enum RoundState
    {
        NoOnePlayed,
        P1Played,
        P2Played,
        P1Won,
        P2Won,
        Tie
    }
}
