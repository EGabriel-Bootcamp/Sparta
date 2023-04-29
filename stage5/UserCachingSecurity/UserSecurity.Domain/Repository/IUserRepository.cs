using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserSecurity.Domain.Entities;

namespace UserSecurity.Domain.Repository
{
    public interface IUserRepository : IGenericRepo<User>
    {
        public void UpdateUser(User user);
    }
}
