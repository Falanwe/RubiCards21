using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Hangman.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HangmanController : ControllerBase
    {
        private readonly ILogger<HangmanController> _logger;
        
        public HangmanController(ILogger<HangmanController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return new WeatherForecast[1];
        }
    }
}