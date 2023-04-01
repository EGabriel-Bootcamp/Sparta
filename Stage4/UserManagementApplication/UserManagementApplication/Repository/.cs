using UserManagementApplication.Entity;

namespace UserManagementApplication.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetByAgeAsync(int age);
        Task<IEnumerable<User>> GetByGenderAsync(string gender);
        Task<IEnumerable<User>> GetByMaritalStatusAsync(string status);
        Task<IEnumerable<User>> GetByAddressAsync(string address);
    }
}
