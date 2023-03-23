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
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;

        public AuthorController(IAuthorRepository authorRepository, IBookRepository bookRepository)
        {
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
        }

        [HttpPost]
        public IActionResult CreateAuthor(CreateAuthorDto authorDto)
        {

            var createdAuthor = _authorRepository.AddAuthor(authorDto);
            if (createdAuthor == null) return BadRequest("Invalid request");

            return CreatedAtAction(nameof(GetAuthor), new { id = createdAuthor.Id }, createdAuthor);
        }

        
        [HttpGet("{id}")]
        public ActionResult GetAuthor(int id)
        {
            var author = _authorRepository.GetAuthorById(id);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        
        [HttpGet]
        public IActionResult GetAllAuthors()
        {
            return Ok(_authorRepository.GetAll());
        }

        [HttpGet("{id}/books")]
        public IActionResult GetBooksAttachedToAnAuthor(int authorId)
        {
            var books = _bookRepository.GetBooksByAuthor(authorId);

            if (!books.Any())
            {
                return NotFound();
            }

            return Ok(books);
        }
    }
}
