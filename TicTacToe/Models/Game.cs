using System;

namespace TicTacToe.Models
{
    public class Game
    {
        public Game() 
        {
            gameId = Guid.NewGuid().ToString();

            isDone = false;
            isP2Turn = true;
            board = new TileState[9];
        }

        public string gameId;

        public bool isDone;
        public bool isP2Turn;
        public TileState[] board = new TileState[9];
    }
}