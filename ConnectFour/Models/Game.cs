using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConnectFour.Models
{
    public class Game
    {
        public string gameId;
        public int PlayerCount;
        public GameState currentState;
        public List<IndividualPlay> plays = new List<IndividualPlay>();
    }
}
