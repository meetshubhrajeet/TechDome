using Microsoft.EntityFrameworkCore;
using TechDome.Models;

namespace TechDome.DatabaseContext
{
    public class MyDatabaseContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "TodoDd");
        }
        public DbSet<TodoItem> Todos { get; set; }
    }
}
