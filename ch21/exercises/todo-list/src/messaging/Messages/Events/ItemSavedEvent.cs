using System;
using ToDoList.Entities;

namespace ToDoList.Messaging.Messages.Events
{
    public class ItemSavedEvent : Message
    {
        public static string MessageSubject = "events.todo.itemsaved";

        public override string Subject { get { return MessageSubject; } }

        public ToDo Item { get; set; }        

        public DateTime SavedAt { get; set; }   

        public ItemSavedEvent(ToDo item)
        {
            Item = item;
            SavedAt = DateTime.UtcNow;
        }
    }
}
