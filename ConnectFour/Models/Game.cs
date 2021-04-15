using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConnectFour.Models
{
    public class Game
    {
        public Game(int newId) { gameId = newId; }

        public int gameId;
        public TileState[] board;
        public IndividualPlay[] plays;
    }
}
