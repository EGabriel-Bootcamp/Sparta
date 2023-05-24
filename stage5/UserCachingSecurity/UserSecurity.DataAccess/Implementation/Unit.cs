using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserSecurityDataAccess.Implementation;
using UserSecurityDataAccess.Context;
using UserSecurityDomain.Repository;

namespace UserSecurityDataAccess.Implementation
{
    public class Unit : IUnit
    {
        private readonly UserContext _context;
        private ICaching cacheService;

        public Unit(UserContext context)
        {
            _context = context;
            User = new UserRepo(_context, cacheService);

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
