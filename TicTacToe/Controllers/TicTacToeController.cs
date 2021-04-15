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
        private static Game lastCreatedGame;
        private static Dictionary<string, Game> games = new Dictionary<string, Game>();

        [HttpGet("game")]
        public async Task<IActionResult> GetGame()
        {
            if (lastCreatedGame == null)
            {            
                   lastCreatedGame = new Game();
                return Ok(await Task.FromResult(new GameForm() { GameId = lastCreatedGame.gameId, PlayerId = 1}));
            }
            else
            {
                games.Add(lastCreatedGame.gameId, lastCreatedGame);
                var id = lastCreatedGame.gameId;

                lastCreatedGame = null;
                return Ok(await Task.FromResult(new GameForm() { GameId = id, PlayerId = 2}));
            }
        }

        [HttpGet("{gameId}")]
        [Produces(typeof(TileState[]))]
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
                if (game.isDone || play.playerId - 1 != Convert.ToInt32(game.isP2Turn)) return BadRequest();

                game.isP2Turn = !game.isP2Turn;
                var index = play.y * 3 + play.x;

                if (game.board[index] != TileState.Empty) return BadRequest();
                else
                {
                    game.board[index] = play.playerId == 1 ? TileState.P1 : TileState.P2;
                    return Ok(await Task.FromResult(game.board));
                }
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
                byte result = 0;
           
                for (var x = 0; x < 3; x++)
                {
                    selection[0] = board[x.Xto2D(0)];
                    selection[1] = board[x.Xto2D(1)];
                    selection[2] = board[x.Xto2D(2)];

                    if (TryEndGame(game, selection, out result)) return Ok(await Task.FromResult(result));
                }

                for (var y = 0; y < 3; y++)
                {
                    selection[0] = board[y.Yto2D(0)];
                    selection[1] = board[y.Yto2D(1)];
                    selection[2] = board[y.Yto2D(2)];

                    if (TryEndGame(game, selection, out result)) return Ok(await Task.FromResult(result));
                }

                selection[0] = board[0];
                selection[1] = board[1 * 3 + 1];
                selection[2] = board[2 * 3 + 2];

                if (TryEndGame(game, selection, out result)) return Ok(await Task.FromResult(result));

                selection[0] = board[2 * 3];
                selection[1] = board[1 * 3 + 1];
                selection[2] = board[2];

                TryEndGame(game, selection, out result);
                return Ok(await Task.FromResult(result));
            }
            else return NotFound();
        }

        private bool TryEndGame(Game game, TileState[] selection, out byte output)
        {
            var result = GetMatch(selection);
            output = result;

            if (result != 0)
            {
                game.isDone = true;
                return true;
            }
            else return false;
                    
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

    public static class Extensions
    {
        public static int Xto2D(this int x, int y)
        {
            return y * 3 + x;
        }

        public static int Yto2D(this int y, int x)
        {
            return y * 3 + x;
        }
    }
}
