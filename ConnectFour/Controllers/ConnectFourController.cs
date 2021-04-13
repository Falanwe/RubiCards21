using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConnectFour.Models;

namespace ConnectFour.Controllers
{
    public class ConnectFourController : ControllerBase
    {
        [ApiController]
        [Route("[controller]")]
        public class ConnectFour : ControllerBase
        {
            [HttpGet("game")]
            public Task<Game> GetGame()
            {
                throw new NotImplementedException();
            }

            [HttpGet("{gameId}")]
            public Task<GameState> GetGameState(int gameId)
            {
                throw new NotImplementedException();
            }

            [HttpPost("{gameId}")]
            public Task<TileState[]> GetGameState(int gameId, [FromBody] IndividualPlay player)
            {
                throw new NotImplementedException();
            }
        }
    }
}
