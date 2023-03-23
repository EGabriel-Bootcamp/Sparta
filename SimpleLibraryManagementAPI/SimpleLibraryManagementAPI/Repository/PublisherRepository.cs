using SimpleLibraryManagementAPI.Data;
using SimpleLibraryManagementAPI.Models;

namespace SimpleLibraryManagementAPI.Repository
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly AppDbContext _context;

        public PublisherRepository(AppDbContext context)
        {
            _context = context;
        }

        public Publisher Create(Publisher publisher)
        {
            var pub = _context.Publishers.FirstOrDefault(p => p.Name == publisher.Name);
            if(pub != null) return null;
            var created = _context.Publishers.Add(publisher);
            _context.SaveChanges();
            

            return created.Entity;
        }

        public List<Publisher> GetAll()
        {
            return _context.Publishers.ToList();
        }

        public Publisher GetPublisherById(int Id)
        {
            return _context.Publishers.FirstOrDefault(x => x.Id == Id);
        }

    }

    public interface IPublisherRepository
    {
        Publisher Create(Publisher publisher);
        Publisher GetPublisherById(int Id);
        List<Publisher> GetAll();
    }
}
