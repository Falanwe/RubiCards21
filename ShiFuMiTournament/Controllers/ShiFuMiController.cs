using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShiFuMiTournament.Models;
using ShiFuMiTournament.Services;

namespace ShiFuMiTournament.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShiFuMiController : ControllerBase
    {
        private readonly IShiFuMiService _service;

        public ShiFuMiController(IShiFuMiService service)
        {
            _service = service;
        }

        [HttpGet("game", Name =nameof(GetGame))]
        public async Task<Game> GetGame()
        {
            return await _service.GetGame();
        }

        [HttpGet("{gameId}", Name = nameof(GetGameState))]
        public async Task<RoundState[]> GetGameState(string gameId)
        {
            return await _service.GetGameState(gameId);
        }

        [HttpPost("{gameId}", Name = nameof(Play))]
        public async Task<RoundState[]> Play(string gameId, [FromBody]IndividualPlay play)
        {
            await _service.Play(gameId, play);
            return await _service.GetGameState(gameId);
        }

        [HttpGet("{gameId}/result", Name = nameof(GetResult))]
        public async Task<GameResult> GetResult(string gameId)
        {
            return await _service.GetResult(gameId);
        }
    }
}
