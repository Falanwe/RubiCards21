﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SampleWebService.Services;

namespace SampleWebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecastCacheService _cache;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            IWeatherForecastCacheService cache)
        {
            _logger = logger;
            _cache = cache;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            return await _cache.GetOrSet(() =>
            {
                var rng = new Random();
                return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
            });
        }

        [HttpPut]
        public async Task ForceWeather([FromBody]List<WeatherForecast> forecasts)
        {
            await _cache.ForceCache(forecasts);
        }

        [HttpPost("EmptyCache")]
        public async Task EmptyCache()
        {
            await _cache.Empty();
        }
    }
}
