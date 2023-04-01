using Microsoft.EntityFrameworkCore;

namespace MyLibraryAPI.Data
{
    public class LibraryContext : DbContext
    { 
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {

        }

        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<AuthorsBook> AuthorsBooks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<AuthorsBook>()
                .HasNoKey();
        }
    }
}
