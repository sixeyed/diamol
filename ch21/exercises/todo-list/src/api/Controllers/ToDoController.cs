using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using ToDoList.Entities;
using ToDoList.Messaging;
using ToDoList.Messaging.Messages.Events;

namespace ToDoList.Api.Controllers
{
    [Route("todo")]
    public class ToDoController : Controller
    {
        protected readonly IConfiguration _configuration;
        
        [HttpPost]
        public IActionResult PostItem([FromBody] ToDo todo)
        {
            //fix up the date:
            todo.DateAdded = DateTime.UtcNow;
            MessageQueue.Publish(new NewItemEvent(todo));
            return Accepted();
        }
    }
}