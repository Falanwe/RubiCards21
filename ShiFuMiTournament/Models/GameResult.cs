using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShiFuMiTournament.Models
{
    public class GameResult
    {
        public bool IsFinished { get; }
        public int P1Won { get; }
        public int P2Won { get; }
        public int Ties { get; }
    }
}
