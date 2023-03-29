using User_Management.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace User_Management.Domain.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
         
        public void updateUser(User user);
    }
}
