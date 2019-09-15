using System;
using System.ComponentModel.DataAnnotations;

namespace Diamol.Ch06.ToDoList.Model
{
    public class ToDo
    {
        [Key]
        public int ToDoId { get; set; }

        [Required]
        [MaxLength(256)]
        public string Item { get; set; }
    }
}