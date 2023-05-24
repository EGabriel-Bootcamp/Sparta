using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserSecurityDataAccess.Context;
using UserSecurityDomain.Entities;
using UserSecurityDomain.Repository;

namespace UserSecurityDataAccess.Implementation
{
    public class UserRepo : GenericRepo<User>, IUserRepository
    {
        private readonly UserContext context;
        private readonly ICaching cacheService;

        public UserRepo(UserContext context, ICaching cacheService) : base(context, cacheService)
        {
            this.context = context;
            this.cacheService = cacheService;
        }

        public void UpdateUser(User user)
        {
            context.Update(user);
        }
    }
}
