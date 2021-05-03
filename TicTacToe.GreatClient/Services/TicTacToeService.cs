using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Models;

namespace TicTacToe.GreatClient.Services
{
    class TicTacToeService
    {
        private const string HOST = "http://10.51.2.23:666";

        public async Task<GameForm> FindGame()
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"{HOST}/TicTacToe/game");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GameForm>(json);
        }

        public async Task<TileState[]> TryGetBoard(string gameId)
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"{HOST}/TicTacToe/{gameId}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            else
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TileState[]>(json);
            }
        }

        public async Task<TileState[]> Play(string gameId, int playerId, int x, int y)
        {
            var client = new HttpClient();

            var play = new IndividualPlay()
            {
                X = x,
                Y = y,
                PlayerId = playerId
            };

            var content = new StringContent(JsonConvert.SerializeObject(play), UTF8Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{HOST}/TicTacToe/{gameId}", content);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound || response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return null;
            }
            else
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TileState[]>(json);
            }
        }

        public async Task<byte> GetResult(string gameId)
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"{HOST}/TicTacToe/{gameId}/result");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return 10;
            }
            else
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<byte>(json);
            }
        }
    }
}
