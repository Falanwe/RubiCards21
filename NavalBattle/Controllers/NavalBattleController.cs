using Microsoft.AspNetCore.Mvc;
using NavalBattle.Models;
using System.Threading.Tasks;

namespace NavalBattle.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class NavalBattleController : ControllerBase
	{
		[HttpGet("game")]
		public Task<Game> GetGame()
		{
			return default;
		}

		[HttpGet("{gameId}")]
		public Task<RoundState[]> GetGameState(int gameId)
		{
			return default;
		}

		[HttpPost("{gameId}")]
		public Task<RoundState[]> Play(int gameId, [FromBody] IndividualPlay play)
		{
			return default;
		}

		[HttpGet("{gameId}/result")]
		public Task<GameResult> GetResult(int gameId)
		{
			return default;
		}
	}
}
