using Microsoft.Extensions.Options;
using Nest;
using ShiFuMiTournament.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShiFuMiTournament.Services
{
    public class ShiFuMiService : IShiFuMiService
    {
        private readonly AppSettings _settings;
        private readonly ElasticClient _client;

        public ShiFuMiService(IOptions<AppSettings> settings)
        {
            _settings = settings.Value;

            var connectionSettings = new ConnectionSettings(new Uri(_settings.ElasticSearchEndpoint));
            connectionSettings.DefaultIndex("shifumi");
            connectionSettings.BasicAuthentication("elastic", _settings.ElasticPassword);
            connectionSettings.ServerCertificateValidationCallback((_, _, _, _) => true);
            _client = new ElasticClient(connectionSettings);
        }

        public async Task<Game> GetGame()
        {
            var response = await _client.SearchAsync<GameRecord>(s => s.Query(q => q.Term(gr => gr.PlayerCount, 1)));
            GameRecord record;
            int playerId;
            if (!response.Hits.Any())
            {
                record = new GameRecord { PlayerCount = 1 };
                var indexResponse = await _client.IndexDocumentAsync(new { PlayerCount = 1 });
                record.Id = indexResponse.Id;
                await _client.IndexDocumentAsync(record);
                playerId = 0;
            }
            else
            {
                record = response.Hits.First().Source;
                record.PlayerCount = 2;
                await _client.IndexDocumentAsync(record);
                playerId = 1;
            }

            return new Game(record.Id, playerId, _settings.RoundsCount);
        }

        public async Task<RoundState[]> GetGameState(string gameId)
        {
            var record = (await _client.GetAsync<GameRecord>(gameId)).Source;

            var result = new RoundState[_settings.RoundsCount];

            for (var i = 0; i < _settings.RoundsCount; i++)
            {
                var player1Play = i < record.Player1Plays.Count ? record.Player1Plays[i] : default(Play?);
                var player2Play = i < record.Player2Plays.Count ? record.Player2Plays[i] : default(Play?);
                result[i] = GetRoundState(player1Play, player2Play);
            }

            return result;
        }

        public async Task<GameResult> GetResult(string gameId)
        {
            var gameState = await GetGameState(gameId);
            var p1won = 0;
            var p2won = 0;
            var ties = 0;
            foreach (var state in gameState)
            {
                switch (state)
                {
                    case RoundState.P1Won:
                        {
                            p1won++;
                            break;
                        }
                    case RoundState.P2Won:
                        {
                            p2won++;
                            break;
                        }
                    case RoundState.Tie:
                        {
                            ties++;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }

            return new GameResult(p1won + p2won + ties == _settings.RoundsCount, p1won, p2won, ties);
        }

        public async Task Play(string gameId, IndividualPlay play)
        {
            var record = (await _client.GetAsync<GameRecord>(gameId)).Source;
            switch (play.PlayerId)
            {
                case 0:
                    {
                        if (record.Player1Plays.Count < _settings.RoundsCount)
                        {
                            record.Player1Plays.Add(play.Play);
                            await _client.IndexDocumentAsync(record);
                        }
                        break;
                    }
                case 1:
                    {
                        if (record.Player2Plays.Count < _settings.RoundsCount)
                        {
                            record.Player2Plays.Add(play.Play);
                            await _client.IndexDocumentAsync(record);
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private RoundState GetRoundState(Play? player1Play, Play? player2Play)
        {
            switch ((player1Play, player2Play))
            {
                case (null, null):
                    return RoundState.NoOnePlayed;
                case (_, null):
                    return RoundState.P1Played;
                case (null, _):
                    return RoundState.P2Played;
                case (Play p1, Play p2) when (p1 == p2):
                    return RoundState.Tie;
                case (Models.Play.Rock, Models.Play.Scissors):
                case (Models.Play.Scissors, Models.Play.Paper):
                case (Models.Play.Paper, Models.Play.Rock):
                    return RoundState.P1Won;
                default:
                    return RoundState.P2Won;
            }
        }
    }
}
