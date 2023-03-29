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
    public class UserRepository : GenericRepository<User> , IUserRepository
    {
        private readonly UserManagement_DbContext _contextVa;

        public UserRepository(UserManagement_DbContext contextVa) : base(contextVa)
        {
            _contextVa = contextVa;
        }

        public void updateUser(User user)
        {
            _contextVa.Update(user);
        }

      
    }
}
