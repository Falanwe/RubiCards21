using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShiFuMiTournament.Models;

namespace ShiFuMiTournament.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShiFuMiController : ControllerBase
    {
        [HttpGet("game")]
        public Task<Game> GetGame()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{gameId}")]
        public Task<RoundState[]> GetGameState(int gameId)
        {
            throw new NotImplementedException();
        }

        [HttpPost("{gameId}")]
        public Task<RoundState[]> Play(int gameId, [FromBody]IndividualPlay play)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{gameId}/result")]
        public Task<GameResult> GetResult(int gameId)
        {
            throw new NotImplementedException();
        }
    }
}
