using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.DataAccess.Context;
using UserManagement.Domain.Repository;

namespace UserManagement.DataAccess.Implementation
{
    public class Unit : IUnit
    {
        private readonly UserContext _context;

        public Unit(UserContext context)
        {
            _context = context;
            User = new UserRepo(_context);

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
