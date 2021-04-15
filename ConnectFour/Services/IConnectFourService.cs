using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConnectFour.Models;

namespace ConnectFour.Services
{
    interface IConnectFourService
    {
        Task<GameForm> GetGame();
        Task<GameState> GetGameState(int gameId);
        Task<List<IndividualPlay>> Play(int gameId, IndividualPlay move);
        Task<List<IndividualPlay>> GetPlays(int gameId);
    }
}
