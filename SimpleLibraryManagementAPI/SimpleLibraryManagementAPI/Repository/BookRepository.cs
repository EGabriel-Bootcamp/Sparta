using SimpleLibraryManagementAPI.Data;
using SimpleLibraryManagementAPI.Dtos;
using SimpleLibraryManagementAPI.Models;

namespace SimpleLibraryManagementAPI.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public Book AddBook(CreateBookDto createBookDto)
        {
            // check and get authors
            if (!createBookDto.AuthorIds.Any()) return null;
            var authors = _context.Authors.Where(author => createBookDto.AuthorIds.Contains(author.Id));
            if(!authors.Any())  return null;
            if(authors.Count() != createBookDto.AuthorIds.Count)  return null;
            if (string.IsNullOrWhiteSpace(createBookDto.Title)) return null;
            var saved = _context.Books.Add(new Book { Title = createBookDto.Title }).Entity;

            // add book-author relationship
            foreach (var author in authors)
            {
                _context.AuthorsBooks.Add(new AuthorsBooks { AuthorId = author.Id, BookId = saved.Id });
            }
            _context.SaveChanges();
            return saved;
        }

        public Book GetBookById(int Id)
        {
            var book = _context.Books.FirstOrDefault(book => book.Id == Id);
            return book;
        }

        public Book GetBookByTitle(string Title)
        {
            var book = _context.Books.FirstOrDefault(book => book.Title == Title);
            return book;
        }

        List<Book> IBookRepository.GetAll()
        {
            return _context.Books.ToList();
        }

        List<Book> IBookRepository.GetBooksByAuthor(int authorId)
        {
            // confirm book with id exists
            var book = GetBookById(authorId);
            if(book == null) return new List<Book>();

            // get ids of authors of book
            var booksIds = _context.AuthorsBooks.Where(ab => ab.AuthorId == authorId);
            if (!booksIds.Any()) return new List<Book>();
            var bookList = new List<Book>();
            foreach (var bookId in booksIds)
            {
                // get details of each author in authorIds list
                var bookInDb = GetBookById(bookId.Id);
                if (bookInDb != null) bookList.Add(bookInDb);
            }
            return bookList;
        }
    }

    public interface IBookRepository
    {
        Book AddBook(CreateBookDto createBookDto);
        Book GetBookById(int Id);
        Book GetBookByTitle(string Title);
        List<Book> GetBooksByAuthor(int authorId);
        List<Book> GetAll();
    }
}
