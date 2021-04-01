using System;
using System.Collections.Generic;

namespace SampleWebService.Services
{
    public interface IWeatherForecastCacheService
    {
        IEnumerable<WeatherForecast> GetOrSet(Func<IEnumerable<WeatherForecast>> factory);
        void Empty();
    }
}