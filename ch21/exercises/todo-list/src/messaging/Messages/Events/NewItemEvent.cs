using System;
using ToDoList.Entities;

namespace ToDoList.Messaging.Messages.Events
{
    public class NewItemEvent : Message
    {
        public static string MessageSubject = "events.todo.newitem";

        public override string Subject { get { return MessageSubject; } }

        public ToDo Item { get; set; }        

        public NewItemEvent(ToDo item)
        {
            Item = item;
        }
    }
}
