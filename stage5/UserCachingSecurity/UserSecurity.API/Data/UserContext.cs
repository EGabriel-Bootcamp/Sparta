using Microsoft.EntityFrameworkCore;

namespace UserSecurity.API.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }
    }
}
