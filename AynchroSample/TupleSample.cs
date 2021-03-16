using System;
using System.Collections.Generic;
using System.Text;

namespace AynchroSample
{
    public enum Move
    {
        Rock,
        Paper,
        Scissor
    }

    public class TupleSample
    {
        public void SwitchTest(Move m1, Move m2)
        {
            switch ((m1, m2))
            {
                case (Move.Rock, Move.Paper):
                case (Move.Paper, Move.Scissor):
                case (Move.Scissor, Move.Rock):
                    {
                        Console.WriteLine("2 wins");
                        break;
                    }
                case (var i, var j) when (i == (Move)((int)j + 1 % 3)):
                    {
                        Console.WriteLine("1 wins");
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Draw");
                        break;
                    }
            }
        }
    }
}
