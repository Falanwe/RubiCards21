using Nest;
using System;
using System.Threading.Tasks;

namespace NavalBattle.Services
{
	public class NavalBattleDatabaseService : INavalBattleService
    {
        public class CacheRecord
        {
            public string Id { get; set; } = "";
            public NavalBattle List { get; set; } = new NavalBattle();
        }

        private readonly ElasticClient _client;

        public NavalBattleDatabaseService()
        {
            var connectionSettings = new ConnectionSettings(new Uri("http://localhost:5001"));
            connectionSettings.DefaultIndex("navalbattle");
            _client = new ElasticClient(connectionSettings);
        }

        public async Task Empty()
        {
            await _client.DeleteAsync<CacheRecord>("cache");
        }

        public async Task<NavalBattle> GetOrSet(Func<NavalBattle> factory)
        {
            var response = await _client.GetAsync<CacheRecord>("cache");
            if (response.Found)
            {
                return response.Source.List;
            }
            else
            {
                var value = factory();
                var creationResponse = await _client.IndexDocumentAsync(new CacheRecord { List = value, Id = "cache" });
                return value;
            }
        }
    }
}
