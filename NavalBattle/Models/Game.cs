using System;

namespace NavalBattle.Models
{
	public class Game
	{
		public string GameId { get; }
		public int PlayerId { get; }
		public int RoundsCount { get; }
		public int[][,] Grids { get; }

		public Game(int playerId, int roundsCount, int[][,] grids)
		{
			GameId = Guid.NewGuid().ToString();
			PlayerId = playerId;
			RoundsCount = roundsCount;
			Grids = grids;
		}
	}
}
