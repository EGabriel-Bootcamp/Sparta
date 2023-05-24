using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserSecurityDomain.Entities;

namespace UserSecurityDomain.Repository
{
    public interface IUserRepository : IGenericRepo<User>
    {
        public void UpdateUser(User user);
    }
}
