using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User_Management.Domain.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        public int Save();   
        
    }
}
