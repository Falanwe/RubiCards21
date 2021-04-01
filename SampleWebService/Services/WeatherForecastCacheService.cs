using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebService.Services
{
    internal class WeatherForecastCacheService : IWeatherForecastCacheService
    {
        private IEnumerable<WeatherForecast>? _cachedValue;
        private readonly object _syncRoot = new object();
        public IEnumerable<WeatherForecast> GetOrSet(Func<IEnumerable<WeatherForecast>> factory)
        {
            IEnumerable<WeatherForecast>? cachedValue = _cachedValue;
            if (cachedValue == null)
            {
                lock (_syncRoot)
                {
                    if (_cachedValue == null)
                    {
                        _cachedValue = factory();
                    }
                    cachedValue = _cachedValue;
                }
            }

            return cachedValue!;
        }

        public void Empty()
        {
            lock (_syncRoot)
            {
                _cachedValue = null;
            }
        }
    }
}
