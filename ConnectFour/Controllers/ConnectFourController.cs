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
            private Dictionary<int, Game> currentGames;
            private Game emptySlotGame;
            private int lastGameId = 0;

            [HttpGet("game")]
            public async Task<IActionResult> GetGame()
            {
                if(emptySlotGame==null)
                {
                    lastGameId++;
                    emptySlotGame = new Game(lastGameId);

                    return Ok(await Task.FromResult(new GameForm() { GameId = emptySlotGame.gameId, PlayerId = 1 }));
                }
                else
                {
                    currentGames.Add(emptySlotGame.gameId, emptySlotGame);
                    return Ok(await Task.FromResult(new GameForm() { GameId = emptySlotGame.gameId, PlayerId = 2 }));
                }

                //throw new NotImplementedException();
            }

            [HttpGet("{gameId}")]
            public async Task<GameState> GetGameState(int gameId)
            {
                //Mettre la logique
                throw new NotImplementedException();
            }

            [HttpPost("{gameId}")]
            public async Task<TileState[]> GetGameState(int gameId, [FromBody] IndividualPlay player)
            {
                //Mettre la logique
                throw new NotImplementedException();
            }
        }
    }
}
