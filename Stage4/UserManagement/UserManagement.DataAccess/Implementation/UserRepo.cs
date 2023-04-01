using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.DataAccess.Context;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Repository;

namespace UserManagement.DataAccess.Implementation
{
    public class UserRepo : GenericRepo<User> , IUserRepository
    {
        private readonly UserContext context;

        public UserRepo(UserContext context) : base(context)
        {
            this.context = context;
        }

        public void UpdateUser(User user)
        {
            context.Update(user);
        }
    }
}
