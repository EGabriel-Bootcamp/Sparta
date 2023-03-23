using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleLibraryManagementAPI.Data;
using SimpleLibraryManagementAPI.Dtos;
using SimpleLibraryManagementAPI.Models;
using SimpleLibraryManagementAPI.Repository;

namespace SimpleLibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IBookRepository _bookRepository;

        public BookController(AppDbContext context, IBookRepository bookRepository)
        {
            _context = context;
            _bookRepository = bookRepository;
        }

        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook(CreateBookDto createBookDto)
        {
            var bookCreated = _bookRepository.AddBook(createBookDto);
            if (bookCreated == null) return BadRequest("Invalid request");
            return CreatedAtAction(nameof(GetBook), new { id = bookCreated.Id }, bookCreated);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = _bookRepository.GetBookById(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            return Ok(_bookRepository.GetAll());
        }

       
    }
}


