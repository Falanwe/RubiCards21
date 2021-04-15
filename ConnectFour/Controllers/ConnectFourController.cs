using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConnectFour.Models;
using ConnectFour.Services;

namespace ConnectFour.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConnectFourController : ControllerBase
    {
        private readonly IConnectFourService connectFourService;

        [HttpGet("game")]
        public async Task<GameForm> GetGame()
        {
            return await connectFourService.GetGame();
        }

        [HttpGet("{gameId}")]
        public async Task<GameState> GetGameState(int gameId)
        {
            return await connectFourService.GetGameState(gameId);
        }

        [HttpPost("{gameId}")]
        public async Task<GameState> Play(int gameId, IndividualPlay move)
        {
            await connectFourService.Play(gameId, move);
            return await connectFourService.GetGameState(gameId);
        }

        [HttpGet("{gameId}/result")]
        public async Task<List<IndividualPlay>> GetPlays(int gameId)
        {
            return await connectFourService.GetPlays(gameId);
        }
    }
}
