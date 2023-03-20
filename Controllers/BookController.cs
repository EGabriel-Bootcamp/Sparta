using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using task_three.Models;

namespace task_three.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController  : Controller
    {
        private readonly DataContext _context;

        public BookController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Books>> Get(int BooksId)
        {
            var book = await _context.Books
                .Where(context => context.Id == BooksId)
                .ToListAsync();
            return Ok(book);
        }

        [HttpGet]
        public async Task<ActionResult<List<Books>>> Get()
        {
            return Ok(await _context.Books.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<Books>> Create(BooksMV modells)
        {

            if (modells == null)
            {
                return BadRequest();
            }
            var pBook = new Books
            {
                Names_Of_Book = modells.Names_Of_Book,
                Date_Of_Production = modells.Date_Of_Production,
                AuthorId = modells.AuthorId
            };

            var userBook = await _context.Publishers.FindAsync(pBook.AuthorId);
            if (userBook == null)
                return NotFound();

            _context.Books.Add(pBook);
            await _context.SaveChangesAsync();

            return await Get(pBook.Id);
        }

        [HttpDelete]
        public async Task<ActionResult<List<Books>>> Delete(int id)
        {
            var BookDel = await _context.Books.FindAsync(id);
            if (BookDel == null)
                return BadRequest("Author not found.");

            _context.Books.Remove(BookDel);
            await _context.SaveChangesAsync();

            return Ok(await _context.Books.ToListAsync());
        }
    }
}
