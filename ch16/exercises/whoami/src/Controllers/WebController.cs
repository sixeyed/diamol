using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace whoami.Controllers
{
    [Produces("application/json")]
    [Route("/")]
    public class WebController : Controller
    {
        private static string _Host = Dns.GetHostName();

        [HttpGet]
        public ActionResult<string> Get()
        {             
            var osDescription = RuntimeInformation.OSDescription;
            var osArchitecture = RuntimeInformation.OSArchitecture.ToString();
            return $"I'm {_Host} running on CPU: {osArchitecture}; OS: {osDescription}";
        }
    }
}
