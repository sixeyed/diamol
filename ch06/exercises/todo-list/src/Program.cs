using Diamol.Ch06.ToDoList.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Diamol.Ch06.ToDoList
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json")
                                .AddEnvironmentVariables()
                                .Build();

            var connectionString = config.GetConnectionString("ToDoDb");

            Console.WriteLine("**************** TO-DO LIST *******************");
            Console.WriteLine("* Enter item to add; ? to list all; ! to exit *");
            Console.WriteLine("***********************************************");
            var cmd = Console.ReadLine();
            while (cmd != "!")
            {
                if (cmd == "?")
                {
                    using (var context = new ToDoContext(connectionString))
                    {
                        foreach (var item in context.ToDos.Select(x => x.Item))
                        {
                            Console.WriteLine(item);
                        }
                    }
                }
                else
                {
                    using (var context = new ToDoContext(connectionString))
                    {
                        context.ToDos.Add(new ToDo
                        {
                            Item = cmd
                        });
                        context.SaveChanges();
                    }
                }
                Console.WriteLine("***");
                cmd = Console.ReadLine();
            }            
        }
    }
}
