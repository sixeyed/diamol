using Microsoft.Extensions.Configuration;
using PowerArgs;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Utilities.HttpCheck
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            var settings = Args.Parse<HttpCheckArgs>(args);
            var targetUrl = settings.Url;
            if (!string.IsNullOrEmpty(settings.UrlFromConfigSetting))
            {
                var config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .AddEnvironmentVariables()
                    .Build();

                targetUrl = config[settings.UrlFromConfigSetting];
            }

            var exitCode = 1;
            try
            {
                using (var client = new HttpClient())
                {
                    var stopwatch = Stopwatch.StartNew();
                    var response = await client.GetAsync(targetUrl);
                    stopwatch.Stop();
                    if (settings.LogSuccess)
                    {
                        Console.WriteLine($"HTTPCheck: status {response.StatusCode}, url {targetUrl}, took {stopwatch.ElapsedMilliseconds}ms");
                    }
                    if (response.StatusCode == HttpStatusCode.OK &&
                        stopwatch.ElapsedMilliseconds < settings.TimeoutMilliseconds)
                    {
                        exitCode = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                if (settings.LogFailure)
                {
                    Console.WriteLine($"HTTPCheck: error. Url {targetUrl}, exception {ex.Message}");
                }
            }
            return exitCode;
        }
    }
}
