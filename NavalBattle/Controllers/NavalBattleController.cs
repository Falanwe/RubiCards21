using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NavalBattle.Models;
using NavalBattle.Services;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NavalBattle.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class NavalBattleController : ControllerBase
	{
		private readonly ILogger<NavalBattleController> logger;
		private readonly INavalBattleService service;

		public NavalBattleController(
			ILogger<NavalBattleController> logger,
			INavalBattleService cache)
		{
			this.logger = logger;
			service = cache;
		}

		[HttpGet("game")]
		public async Task<NavalBattle> GetGame()
		{
			return await service.GetOrSet(() =>
			{
				return new NavalBattle();
			});
		}

		[HttpGet("{gameId}")]
		public async Task<RoundState[]> GetGameState(int gameId)
		{
			return default;
		}

		[HttpPost("{gameId}")]
		public async Task<RoundState[]> Play(int gameId, [FromBody] IndividualPlay play)
        {
            // POST or GET
            using (var client = new HttpClient())
            {
				var a = await HttpContext.Request.ReadFormAsync();
				Console.WriteLine("salut");

			}
            return default;
		}

		[HttpGet("{gameId}/result")]
		public async Task<GameResult> GetResult(int gameId)
		{
			return default;
		}

		[HttpPost("EmptyCache")]
		public async Task EmptyCache()
		{
			await service.Empty();
		}
	}
}
