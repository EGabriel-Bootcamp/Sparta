using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserSecurityDomain.Entities;

namespace UserSecurityDataAccess.Context
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<AuthUser> AuthUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AuthUser>(
           eb =>
           {
               eb.HasNoKey();
           });
        }

    }
}
