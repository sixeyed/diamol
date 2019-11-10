using Microsoft.AspNetCore.Mvc;

namespace Numbers.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            if (Status.Healthy)
            {                
                return Ok("Ok");
            }
            else
            {                
                return StatusCode(500);
            }
        }
    }
}
