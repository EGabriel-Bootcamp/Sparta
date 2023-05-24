using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserSecurityDataAccess.Context;
using UserSecurityDomain.Repository;

namespace UserSecurityDataAccess.Implementation
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly UserContext context;
        private readonly ICaching cacheService;

        public GenericRepo(UserContext context, ICaching cacheService)
        {
            this.context = context;
            this.cacheService = cacheService;
        }
        public void Create(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            context.Set<T>().RemoveRange(entities);
        }

        public IEnumerable<T> GetAll()
        {
            var cacheResult = cacheService.GetData<IEnumerable<T>>("key");
            if (cacheResult != null)
            {
                return cacheResult;
            }
            var result = context.Set<T>().ToList();
            var expiryTime = DateTimeOffset.Now.AddSeconds(60);
            cacheService.SetData<IEnumerable<T>>("key", result, expiryTime);

            return result;
        }

        public IEnumerable<T> GetBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().Where(predicate).ToList();
        }

        public T GetById(int id)
        {
            return context.Set<T>().Find(id);
        }

        public void Update(T entity)
        {
            context.Set<T>().Update(entity);
        }
    }
}
