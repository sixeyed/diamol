using Microsoft.Extensions.Configuration;
using NATS.Client;
using System;
using System.Threading;
using ToDoList.Messaging;
using ToDoList.Messaging.Messages.Events;

namespace ToDoList.AuditHandler.Workers
{
    public class QueueWorker
    {
        private static ManualResetEvent _ResetEvent = new ManualResetEvent(false);
        private const string QUEUE_GROUP = "audit-handler";
        private readonly IConfiguration _config;

        public QueueWorker(IConfiguration config)
        {
            _config = config;
        }

        public void Start()
        {
            Console.WriteLine($"Connecting to message queue url: {_config["MessageQueue:Url"]}");
            using (var connection = MessageQueue.CreateConnection())
            {
                var subscription = connection.SubscribeAsync(NewItemEvent.MessageSubject, QUEUE_GROUP);
                subscription.MessageHandler += AuditItem;
                subscription.Start();
                Console.WriteLine($"Listening on subject: {NewItemEvent.MessageSubject}, queue: {QUEUE_GROUP}");

                _ResetEvent.WaitOne();
                connection.Close();
            }
        }

        private void AuditItem(object sender, MsgHandlerEventArgs e)
        {         
            var eventMessage = MessageHelper.FromData<NewItemEvent>(e.Message.Data);
            Console.WriteLine($"AUDIT @ {eventMessage.Item.DateAdded}: {eventMessage.Item.Item}");
        }
    }
}
