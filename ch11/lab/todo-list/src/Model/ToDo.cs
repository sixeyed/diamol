using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Model
{
    public class ToDo
    {
        [Key]
        public int ToDoId { get; set; }

        [Required]
        [MaxLength(256)]
        [StringLength(256, ErrorMessage = "Item is too big")]
        public string Item { get; set; }

        public DateTime DateAdded { get; set; }
    }
}