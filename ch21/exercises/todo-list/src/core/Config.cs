using Microsoft.Extensions.Configuration;
using System;

namespace ToDoList.Core
{
    public class Config
    {
        public static IConfiguration Current { get; private set; }

        static Config()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            AddOverrideFile(builder);
            Current = builder.Build();
        }

        public static void AddOverrideFile(IConfigurationBuilder config)
        {
            config.AddJsonFile("config-override/local.json", optional: true);
        }
    }
}
