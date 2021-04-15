using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShiFuMiTournament.Models
{
    public class Game
    {
        public Game(string gameId, int playerId, int roundsCount)
        {
            GameId = gameId;
            PlayerId = playerId;
            RoundsCount = roundsCount;
        }
        public string GameId { get; }
        public int PlayerId { get; }
        public int RoundsCount { get; }
    }
}
