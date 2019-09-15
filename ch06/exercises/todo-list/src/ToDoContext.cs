using Diamol.Ch06.ToDoList.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;

namespace Diamol.Ch06.ToDoList
{
    public class ToDoContext : DbContext
    {
        private readonly string _connectionString;

        public DbSet<ToDo> ToDos { get; set; }

        public ToDoContext(string connectionString) : base()
        {
            _connectionString = connectionString;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
