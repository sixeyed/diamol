using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NATS.Client;
using System;
using System.Threading;
using ToDoList.Messaging;
using ToDoList.Messaging.Messages.Events;
using ToDoList.Model;

namespace ToDoList.SaveHandler.Workers
{
    public class QueueWorker
    {
        private static ManualResetEvent _ResetEvent = new ManualResetEvent(false);
        private const string QUEUE_GROUP = "mutating-handler";
        private readonly IConfiguration _config;
        private readonly IServiceProvider _serviceProvider;

        public QueueWorker(IConfiguration config, IServiceProvider serviceProvider)
        {
            _config = config;
            _serviceProvider = serviceProvider;
        }

        public void Start()
        {
            Console.WriteLine($"Connecting to message queue url: {_config["MessageQueue:Url"]}");
            using (var connection = MessageQueue.CreateConnection())
            {
                var subscription = connection.SubscribeAsync(ItemSavedEvent.MessageSubject, QUEUE_GROUP);
                subscription.MessageHandler += MutateItem;
                subscription.Start();
                Console.WriteLine($"Listening on subject: {ItemSavedEvent.MessageSubject}, queue: {QUEUE_GROUP}");

                _ResetEvent.WaitOne();
                connection.Close();
            }
        }

        private void MutateItem(object sender, MsgHandlerEventArgs e)
        {
            Console.WriteLine($"Received message, subject: {e.Message.Subject}");
            var eventMessage = MessageHelper.FromData<ItemSavedEvent>(e.Message.Data);
            Console.WriteLine($"Mutating item, event ID: {eventMessage.CorrelationId}");

            try
            {
                using (var context = _serviceProvider.GetService<ToDoContext>())
                {
                    var todo = context.ToDos.Find(eventMessage.Item.ToDoId);
                    todo.Item = "Write a nice review for Learn Docker in a Month of Lunches :)";                    
                    context.SaveChanges();
                }
                Console.WriteLine($"Item mutated; ID: {eventMessage.Item.ToDoId}; event ID: {eventMessage.CorrelationId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Mutation FAILED; event ID: {eventMessage.CorrelationId}; exception: {ex}");
            }
        }
    }
}
