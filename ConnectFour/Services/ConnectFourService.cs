using ConnectFour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace ConnectFour.Services
{
    public class ConnectFourService : IConnectFourService
    {
        

        public Task<GameForm> GetGame()
        {
            throw new NotImplementedException();
        }

        public Task<GameState> GetGameState(int gameId)
        {
            throw new NotImplementedException();
        }

        public Task<IndividualPlay[]> GetPlays(int gameId)
        {
            throw new NotImplementedException();
        }

        public Task Play(int gameId, IndividualPlay move)
        {
            throw new NotImplementedException();
        }
    }
}
