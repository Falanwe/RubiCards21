namespace Hangman
{
    public class Game
    {
        public int GameId { get; }
        public int PlayerId { get; }
        public int RoundsCount { get; }
        public int ErrorsCount { get; }
        public string UsedLetters { get; }
    }
}