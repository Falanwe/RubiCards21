namespace NavalBattle.Models
{
	public class Game
	{
		public int GameId { get; }
		public int PlayerId { get; }
		public int RoundsCount { get; }
    }
    public class IndividualPlay
    {
        public int PlayerId { get; }
        public Play Play { get; }
    }
}
