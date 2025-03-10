using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace Numbers.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RngController : ControllerBase
    {
        private static Random _Random = new Random();
        private static int _CallCount;
        private static int _BreakAfterCallCount;

        private readonly ILogger<RngController> _logger;

        public RngController(IConfiguration config, ILogger<RngController> logger)
        {
            _BreakAfterCallCount = config.GetValue<int>("App:BreakAfterCallCount", 3);
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _CallCount++;
            if (_BreakAfterCallCount > -1 && _CallCount > _BreakAfterCallCount)
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
