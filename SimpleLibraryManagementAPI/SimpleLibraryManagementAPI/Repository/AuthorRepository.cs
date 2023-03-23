using SimpleLibraryManagementAPI.Data;
using SimpleLibraryManagementAPI.Dtos;
using SimpleLibraryManagementAPI.Models;

namespace SimpleLibraryManagementAPI.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _context;

        public AuthorRepository(AppDbContext context)
        {
            _context = context;
        }

        public Author AddAuthor(CreateAuthorDto authorDto)
        {
            // conform publisher with PublisherId exists
            var publisher = _context.Publishers.FirstOrDefault(x => x.Id == authorDto.PublisherId);
            if (publisher == null) return null;

            // add author
            var createdAuthor = _context.Authors.Add(new Author { Name = authorDto.Name}).Entity;

            // add author-publisher relationship
            var authorPublisher = _context.PublisherAuthors.Add(new PublisherAuthors { PublisherId = authorDto.PublisherId, AuthorId = createdAuthor.Id });
            _context.SaveChanges();
            return createdAuthor;
        }

        public List<Author> GetAllAuthorOfBook(int bookId)
        {
            var authorsIds = _context.AuthorsBooks.Where(ab => ab.BookId == bookId);
            if (!authorsIds.Any()) return new List<Author>();
            var authorList = new List<Author>();
            foreach (var author in authorsIds)
            {
                // get details of each author in authorIds list
                var authorInDb = GetAuthorById(author.AuthorId);
                if (authorInDb != null) authorList.Add(authorInDb);
            }
            return authorList;
        }

        public Author GetAuthorById(int Id)
        {
            return _context.Authors.FirstOrDefault(author => author.Id == Id);
        }

        public List<Author> GetAuthorsOfPublisher(int publisherId)
        {
            var authorsOfPublisher = _context.PublisherAuthors.Where(pa => pa.PublisherId == publisherId);
            if (!authorsOfPublisher.Any()) return new List<Author>();
            var authors = new List<Author>();
            foreach (var authorId in authorsOfPublisher)
            {
                var author = GetAuthorById(authorId.AuthorId);
                authors.Add(author);
            }
            return authors;
        }

        List<Author> IAuthorRepository.GetAll()
        {
            return _context.Authors.ToList();
        }
    }

    public interface IAuthorRepository
    {
        Author GetAuthorById(int Id);
        Author AddAuthor(CreateAuthorDto authorDto);
        List<Author> GetAllAuthorOfBook(int bookId);
        List<Author> GetAuthorsOfPublisher(int publisherId);

        List<Author> GetAll();
    }
}
