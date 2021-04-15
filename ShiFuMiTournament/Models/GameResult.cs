using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShiFuMiTournament.Models
{
    public class GameResult
    {
        public GameResult(bool isFinished, int p1Won, int p2won, int ties)
        {
            IsFinished = isFinished;
            P1Won = p1Won;
            P2Won = p2won;
            Ties = ties;
        }

        public bool IsFinished { get; }
        public int P1Won { get; }
        public int P2Won { get; }
        public int Ties { get; }
    }
}
