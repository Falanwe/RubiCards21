using System;

namespace TicTacToe.Models
{
    public class Game
    {
        public Game() { gameId = Guid.NewGuid().ToString(); }

        public string gameId;
        public TileState[,] board = new TileState[3,3];
    }
}