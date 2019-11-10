using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Numbers.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RngController : ControllerBase
    {
        private static Random _Random = new Random();
        private static int _CallCount;

        private readonly ILogger<RngController> _logger;

        public RngController(ILogger<RngController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _CallCount++;
            if (_CallCount > 3)
            {
                Status.Healthy = false;
            }

            if (Status.Healthy)
            {
                var n = _Random.Next(0,100);
                _logger.LogDebug($"Returning random number: {n}");
                return Ok(n);
            }
            else
            {
                _logger.LogWarning("Unhealthy!");
                return StatusCode(500);
            }
        }
    }
}
