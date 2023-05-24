using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserSecurityDomain.Repository
{
    public interface IUnit : IDisposable
    {
        IUserRepository User { get; }
        int Save();
    }
}
