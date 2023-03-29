using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User_Management.DataAccess.User_Context;
using User_Management.Domain.Entities;
using User_Management.Domain.Repository;

namespace User_Management.DataAccess.Implementation
{
    public class UnitOfWork :IUnitOfWork
    {
        private readonly UserManagement_DbContext _context;

        public UnitOfWork(UserManagement_DbContext context)
        {
            _context = context;
            User = new UserRepository(_context);
        }

        public IUserRepository User { get; private set; }
        

        public int Save()
        {
            
            return _context.SaveChanges();
        }


       
        public void Dispose()
        {
            _context.Dispose();
        }

     
    }
}
