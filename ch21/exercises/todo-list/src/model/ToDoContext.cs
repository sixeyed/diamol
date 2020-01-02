using Microsoft.EntityFrameworkCore;
using ToDoList.Entities;

namespace ToDoList.Model
{
    public class ToDoContext : DbContext
    {
        public DbSet<ToDo> ToDos { get; set; }

        public ToDoContext() : base()
        {
            Database.EnsureCreated();
        }

        public ToDoContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
