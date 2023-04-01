using Microsoft.EntityFrameworkCore;
using System;
using User_Management.Data;
using User_Management.Domain;

namespace User_Management.Repository
{
    public class UserRepository<T> : IUserRepository<T> where T : class
    {
        private readonly RepositoryContext _dbContext;

        public UserRepository(RepositoryContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> Create(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public Task<IEnumerable<T>> FilterUsers(int? age, string gender, string maritalStatus, string residence)
        {
            var filteredUsers = _dbContext.Users.Where(u => u.Age == age && u.Gender == gender && u.MaritalStatus == maritalStatus && u.Residence == residence);

            return (Task<IEnumerable<T>>)filteredUsers;
        }
    }

}
