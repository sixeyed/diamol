using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Pi.Web.Math;
using Pi.Web.Models;

namespace Pi.Web.Controllers
{
    public class PiController : Controller
    {
        public IActionResult Index(int? dp = 6)
        {
            var stopwatch = Stopwatch.StartNew();
            HighPrecision.Precision = dp.Value;
            HighPrecision first = 4 * Atan.Calculate(5);
            HighPrecision second = Atan.Calculate(239);

            var pi = 4 * (first - second);

            var model = new PiViewModel
            {
                DecimalPlaces = dp.Value,
                Value = pi.ToString(),
                ComputeMilliseconds = stopwatch.ElapsedMilliseconds,
                ComputeHost = Environment.MachineName
            };

            return View(model);
        }
    }
}