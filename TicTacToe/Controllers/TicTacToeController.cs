using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Models;

namespace TicTacToe.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicTacToeController : Controller
    {
        private Game lastCreatedGame;
        private Dictionary<string, Game> games = new Dictionary<string, Game>();

        [HttpGet("game")]
        public async Task<IActionResult> GetGame()
        {
            if (lastCreatedGame == null)
            {
                lastCreatedGame = new Game();
                return Ok(await Task.FromResult(new GameForm() { gameId = lastCreatedGame.gameId, playerId = 1}));
            }
            else
            {
                games.Add(lastCreatedGame.gameId, lastCreatedGame);
                var id = lastCreatedGame.gameId;

                lastCreatedGame = null;
                return Ok(await Task.FromResult(new GameForm() { gameId = id, playerId = 2}));
            }
        }

        [HttpGet("{gameId}")]
        [Produces(typeof(TileState[,]))]
        public async Task<IActionResult> GetBoard(string gameId)
        {
            if (games.TryGetValue(gameId, out var game)) return Ok(await Task.FromResult(game.board));
            else return NotFound();
        }

        [HttpPost("{gameId}")]
        public async Task<IActionResult> Play(string gameId, [FromBody]IndividualPlay play)
        {
            if (games.TryGetValue(gameId, out var game))
            {
                if (game.board[play.x, play.y] != TileState.Empty) return BadRequest();
                else return Ok(await Task.FromResult(game.board));
            }
            else return NotFound();
        }

        [HttpGet("{gameId}/result")]
        public async Task<IActionResult> GetResult(string gameId)
        {
            if (games.TryGetValue(gameId, out var game))
            {
                var board = game.board;
                var selection = new TileState[3];
                var result = 0;

                for (var x = 0; x < board.GetLength(0); x++)
                {
                    selection[0] = board[x, 0];
                    selection[1] = board[x, 1];
                    selection[2] = board[x, 2];

                    result = GetMatch(selection);
                    if (result != 0) return Ok(await Task.FromResult(result));
                }

                for (var y = 0; y < board.GetLength(0); y++)
                {
                    selection[0] = board[0, y];
                    selection[1] = board[1, y];
                    selection[2] = board[2, y];

                    result = GetMatch(selection);
                    if (result != 0) return Ok(await Task.FromResult(result));
                }

                selection[0] = board[0, 0];
                selection[1] = board[1, 1];
                selection[2] = board[2, 2];

                result = GetMatch(selection);
                if (result != 0) return Ok(await Task.FromResult(result));

                selection[0] = board[0, 2];
                selection[1] = board[1, 1];
                selection[2] = board[2, 0];

                result = GetMatch(selection);
                return Ok(await Task.FromResult(result));
            }
            else return NotFound();
        }

        private byte GetMatch(TileState[] tileStates)
        {
            var result = tileStates[0];
            if (result == 0) return 0;

            for (var i = 1; i < tileStates.Length; i++)
            {
                if (tileStates[i] != result) return 0;
            }

            return (byte)result;
        }
    }
}
