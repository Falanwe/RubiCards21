using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TicTacToe.GreatClient.Services;
using TicTacToe.Models;

namespace TicTacToe.GreatClient
{
    class Program
    {
        private const int refreshDelay = 300;
        private static bool isMyTurn;

        static async Task Main(string[] args)
        {
            var service = new TicTacToeService();
            var form = await service.FindGame();
            isMyTurn = form.PlayerId == 2;

            Console.WriteLine(JsonConvert.SerializeObject(form));

            TileState[] board = null;
            Console.WriteLine("Waiting for game...");
            while (board == null)
            {
                await Task.Delay(refreshDelay);
                board = await service.TryGetBoard(form.GameId);
            }
            Console.Clear();

            byte result = 0;
            while (result == 0)
            {
                var index = 0;
                for (var y = 0; y < 3; y++)
                {
                    for (var x = 0; x < 3; x++)
                    {
                        switch (board[index])
                        {
                            case TileState.Empty:
                                Console.Write("#");
                                break;

                            case TileState.P1:
                                Console.Write("X");
                                break;

                            case TileState.P2:
                                Console.Write("O");
                                break;
                        }
                        index++;
                    }

                    Console.WriteLine();
                }

                if (!isMyTurn)
                {
                    await Task.Delay(refreshDelay);
                    var newBoard = await service.TryGetBoard(form.GameId);

                    for (var i = 0; i < newBoard.Length; i++)
                    {
                        if (board[i] == newBoard[i]) continue;

                        result = await service.GetResult(form.GameId);
                        if (result != 0) break;

                        isMyTurn = true;
                        board = newBoard;

                        break;
                    }

                    Console.Clear();
                    continue;
                }

                var command = Console.ReadLine();
                Console.Clear();

                if (command.Length == 0 || command[0] != '/') return;

                var data = command.Split(" ");
                data[0] = data[0].Substring(1, data[0].Length - 1);

                switch (data[0])
                {
                    case "play":

                        if (!isMyTurn) break;
                        var x = Convert.ToInt32(data[1]);
                        var y = Convert.ToInt32(data[2]);

                        var newBoard = await service.Play(form.GameId, form.PlayerId, x, y);
                        if (newBoard == null) break;

                        result = await service.GetResult(form.GameId);
                        if (result != 0) break;

                        board = newBoard;
                        isMyTurn = false;

                        break;
                }
            }

            Console.Clear();

            var message = result != form.PlayerId ? "Lost" : "Won";
            Console.WriteLine($"You {message}!");
        }
    }
}
