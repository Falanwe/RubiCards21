using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleWebService.Services
{
    public interface IWeatherForecastCacheService
    {
        Task<IEnumerable<WeatherForecast>> GetOrSet(Func<IEnumerable<WeatherForecast>> factory);
        Task Empty();
        Task ForceCache(List<WeatherForecast> forecasts);
    }
}