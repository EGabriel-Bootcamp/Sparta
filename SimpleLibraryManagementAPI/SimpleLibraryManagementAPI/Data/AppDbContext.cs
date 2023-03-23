using Microsoft.EntityFrameworkCore;
using SimpleLibraryManagementAPI.Models;

namespace SimpleLibraryManagementAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Book>Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<AuthorsBooks> AuthorsBooks{ get; set; }
        public DbSet<PublisherAuthors> PublisherAuthors{ get; set; }
       
    }
}
