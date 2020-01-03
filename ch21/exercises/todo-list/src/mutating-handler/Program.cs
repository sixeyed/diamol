using Microsoft.Extensions.DependencyInjection;
using ToDoList.Core;
using ToDoList.SaveHandler.Workers;

namespace ToDoList.SaveHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton(Config.Current)
                .AddSingleton<QueueWorker>()
                .AddToDoContext(Config.Current, ServiceLifetime.Transient)
                .BuildServiceProvider();

            var worker = serviceProvider.GetService<QueueWorker>();
            worker.Start();
        }
    }
}
