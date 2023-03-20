using Microsoft.EntityFrameworkCore;
using task_three.Models;

namespace task_three.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Authors> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Books> Books { get; set; }



    }
}
