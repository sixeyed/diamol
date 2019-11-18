using System;
using System.Threading;
using System.Timers;
using Microsoft.Extensions.Configuration;

namespace Diamol.Chapter12.TimeCheck
{
    class Program
    {
        private static ManualResetEvent _ResetEvent = new ManualResetEvent(false);
        private static System.Timers.Timer _Timer;
        private static string _Version;

        public static void Main()
        {
            var config = new ConfigurationBuilder()
                             .AddJsonFile("appsettings.json")
                             .AddEnvironmentVariables()
                             .AddJsonFile("configs/config.json", optional: true)
                             .AddJsonFile("secrets/secret.json", optional: true)
                             .Build();

            _Version = config["Application:Version"];

            var intervalSeconds = int.Parse(config["Timer:IntervalSeconds"]) * 1000;
            using (var timer = new System.Timers.Timer(intervalSeconds))
            {
                timer.Elapsed += WriteTimeCheck;
                timer.Enabled = true;
                _ResetEvent.WaitOne();
            }
        }

        private static void WriteTimeCheck(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine($"App version: {_Version}; time check: {e.SignalTime.ToString("HH:mm.ss")}");
        }
    }
}
