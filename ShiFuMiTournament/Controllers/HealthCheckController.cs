using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShiFuMiTournament.Controllers
{
    [ApiController]
    [Route("")]
    public class HealthCheckController : Controller
    {

        public IActionResult Get()
        {
            return Ok("it works!");
        }
    }
}
