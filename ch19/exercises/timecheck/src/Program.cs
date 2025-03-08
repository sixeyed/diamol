using System;
using System.Threading;
using System.Timers;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Diamol.Chapter12.TimeCheck
{
    class Program
    {
        private static ManualResetEvent _ResetEvent = new ManualResetEvent(false);
        private static string _Version;
        private static string _Env;

        public static void Main()
        {
            var config = new ConfigurationBuilder()
                             .AddJsonFile("appsettings.json")
                             .AddEnvironmentVariables()
                             .AddJsonFile("configs/config.json", optional: true)
                             .AddJsonFile("secrets/secret.json", optional: true)
                             .Build();

            _Version = config["Application:Version"];
            _Env = config["Application:Environment"];
            var intervalSeconds = int.Parse(config["Timer:IntervalSeconds"]) * 1000;
            var writeToConsole = bool.Parse(config["Output:Console"]);

            Log.Logger = new LoggerConfiguration()
                                .MinimumLevel.Information()
                                .WriteTo.File("/logs/timecheck.log", shared: true, flushToDiskInterval: TimeSpan.FromSeconds(intervalSeconds))
                                .CreateLogger();

            using (var timer = new System.Timers.Timer(intervalSeconds))
            {         
                if (writeToConsole)
                {
                    timer.Elapsed += WriteToConsole;
                }
                else
                {    
                    timer.Elapsed += WriteLog;
                }
                timer.Enabled = true;
                _ResetEvent.WaitOne();
            }
        }

        
        private static void WriteToConsole(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine($"Environment: {_Env}; version: {_Version}; time check: {e.SignalTime.ToString("HH:mm.ss")}");
        }

        private static void WriteLog(Object source, ElapsedEventArgs e)
        {
            Log.Information("Environment: {environment}; version: {version}; time check: {timestamp}",
                            _Env, _Version, e.SignalTime.ToString("HH:mm.ss"));
        }
    }
}
