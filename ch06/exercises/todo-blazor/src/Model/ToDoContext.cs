using Microsoft.EntityFrameworkCore;

namespace ToDoList.Model
{
    public class ToDoContext : DbContext
    {
        public DbSet<ToDo> ToDos { get; set; }

        public ToDoContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
