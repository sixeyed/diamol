using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Pi.Math;
using Pi.Web.Models;
using System;
using System.Diagnostics;

namespace Pi.Web.Controllers
{
    public class PiController : Controller
    {
        private readonly int _defaultDp;

        public PiController(IConfiguration config)
        {
            _defaultDp = int.Parse(config["Computation:Default:DecimalPlaces"]);
        }

        public IActionResult Index(int? dp)
        {
            if (dp == null)
            {
                dp = _defaultDp;
            }

            var stopwatch = Stopwatch.StartNew();

            var pi = MachinFormula.Calculate(dp.Value);

            var model = new PiViewModel
            {
                DecimalPlaces = dp.Value,
                Value = pi.ToString(),
                ComputeMilliseconds = stopwatch.ElapsedMilliseconds,
                ComputeHost = Environment.MachineName,
                HostCpuCount = Environment.ProcessorCount
            };

            return View(model);
        }
    }
}