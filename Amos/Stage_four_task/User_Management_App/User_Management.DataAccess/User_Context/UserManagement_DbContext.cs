using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User_Management.Domain.Entities;

namespace User_Management.DataAccess.User_Context
{
    public class UserManagement_DbContext  :DbContext
    {
        public UserManagement_DbContext(DbContextOptions<UserManagement_DbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                    new User { Id = 1, FirstName = "Amos" , LastName = "Iqout" , Gender = "Male" , MartialStatus = "Single" , Age = 17 , StateOfResident = "Akwa Ibom"},
                    new User { Id = 2, FirstName = "John", LastName = "Smith", Gender = "Male", MartialStatus = "Married", Age = 35, StateOfResident = "Lagos" },
                    new User { Id = 3, FirstName = "Victor", LastName = "James", Gender = "Female", MartialStatus = "Single", Age = 18, StateOfResident = "Cross Rivers" },
                    new User { Id = 4, FirstName = "Blessing", LastName = "James", Gender = "Female", MartialStatus = "Single", Age = 20, StateOfResident = "Rivers" },
                    new User { Id = 5, FirstName = "Amarachi", LastName = "Favour", Gender = "Female", MartialStatus = "Married", Age = 50, StateOfResident = "Abia" },
                    new User { Id = 6, FirstName = "Stephen", LastName = "Michael", Gender = "Male", MartialStatus = "Married", Age = 65, StateOfResident = "Delta" }
                );
        }
    }
}
