using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Diamol.Ch06.FileDisplay
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json")
                                .AddEnvironmentVariables()
                                .Build();

            var inputPath = config["File:Path"];
            var message = File.ReadAllText(inputPath);
            Console.WriteLine(message);

            var waitSeconds = int.Parse(config["Wait:Seconds"]);
            Task.Delay(waitSeconds*1000).Wait();
        }
    }
}