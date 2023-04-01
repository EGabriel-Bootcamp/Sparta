namespace User_Management.Repository
{
    public interface IUserRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task Delete(T entity);
        Task<IEnumerable<T>> FilterUsers(int? age, string gender, string maritalStatus, string residence);
    }





}
