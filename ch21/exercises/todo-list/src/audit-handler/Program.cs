using Microsoft.Extensions.DependencyInjection;
using ToDoList.AuditHandler.Workers;
using ToDoList.Core;

namespace ToDoList.AuditHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton(Config.Current)
                .AddSingleton<QueueWorker>()
                .BuildServiceProvider();

            var worker = serviceProvider.GetService<QueueWorker>();
            worker.Start();
        }
    }
}
