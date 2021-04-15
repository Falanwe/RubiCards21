using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConnectFour.Models
{
    public class GameForm
    {
        public GameForm(string _gameId, int _playerId)
        {
            GameId = _gameId;
            PlayerId = _playerId;
        }
        public string GameId;
        public int PlayerId;
    }
}
