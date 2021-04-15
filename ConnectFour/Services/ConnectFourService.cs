using ConnectFour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Nest;

namespace ConnectFour.Services
{
    public class ConnectFourService : IConnectFourService
    {
        private readonly ElasticClient client;

        public ConnectFourService()
        {
            var connectionSettings = new ConnectionSettings(new Uri("https://localhost:5001"));
            connectionSettings.DefaultIndex("connectFour");
            client = new ElasticClient(connectionSettings);
        }

        public async Task<GameForm> GetGame()
        {
            var response = await client.SearchAsync<Game>(s => s.Query(q => q.Term(gr => gr.PlayerCount, 1)));
            Game record;
            int playerId;
            if(!response.Hits.Any())
            {
                record = new Game() { PlayerCount = 1 };
                var indexResponde = await client.IndexDocumentAsync(new { playerCount = 1 });
                playerId = 0;
                record.gameId = indexResponde.Id;
                await client.IndexDocumentAsync(record);
            }
            else
            {
                record = response.Hits.First().Source;
                record.PlayerCount = 2;
                await client.IndexDocumentAsync(record);
                playerId = 1;
            }
            return new GameForm(record.gameId, playerId);
        }

        public async Task<GameState> GetGameState(int gameId)
        {
            var record = (await client.GetAsync<Game>(gameId)).Source;

            return record.currentState;
        }

        public async Task<List<IndividualPlay>> GetPlays(int gameId)
        {
            var record = (await client.GetAsync<Game>(gameId)).Source;

            return record.plays;
        }

        public async Task<List<IndividualPlay>> Play(int gameId, IndividualPlay move)
        {
            var record = (await client.GetAsync<Game>(gameId)).Source;

            record.plays.Add(move);
            return record.plays;
        }
    }
}
