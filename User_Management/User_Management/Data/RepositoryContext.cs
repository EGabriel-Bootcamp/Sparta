using Microsoft.EntityFrameworkCore;
using User_Management.Domain;

namespace User_Management.Data
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
            : base(options)  
        {

        }
        
        public DbSet<User> Users { get; set; }
    }
}
