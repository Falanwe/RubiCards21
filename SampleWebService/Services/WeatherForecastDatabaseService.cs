using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebService.Services
{
    public class WeatherForecastDatabaseService : IWeatherForecastCacheService
    {
        public class CacheRecord
        {
            public string Id { get; set; } = "";
            public List<WeatherForecast> List { get; set; } = new List<WeatherForecast>();
        }

        private readonly ElasticClient _client;

        public WeatherForecastDatabaseService()
        {
            var connectionSettings = new ConnectionSettings(new Uri("http://localhost:9200"));
            connectionSettings.DefaultIndex("weatherforecast");
            _client = new ElasticClient(connectionSettings);
        }

        public async Task Empty()
        {
            await _client.DeleteAsync<CacheRecord>("cache");
        }

        public async Task<IEnumerable<WeatherForecast>> GetOrSet(Func<IEnumerable<WeatherForecast>> factory)
        {
            var response = await _client.GetAsync<CacheRecord>("cache");
            if (response.Found)
            {
                return response.Source.List;
            }
            else
            {
                var value = factory().ToList();
                var creationResponse = await _client.IndexDocumentAsync(new CacheRecord { List = value, Id ="cache" });
                return value;
            }
        }

        public Task ForceCache(List<WeatherForecast> forecasts)
        {
            throw new NotImplementedException();
        }
    }
}
