using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryAPi.Data;
using LibraryAPi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MyLibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly LibraryContext _context;

        public AuthorsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            return await _context.Authors.ToListAsync();
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return author;
        }

        // GET: api/Authors/5
        [HttpGet("{id}/AuthorsBooks")]
        public async Task<ActionResult<dynamic>> GetAuthorsBooks(int id)
        {
            var authorsBook = await _context.AuthorsBooks
                .Where(a => a.AuthorId == id)
                .Include(a => a.Book)
                .ToListAsync();

            if (authorsBook == null || !authorsBook.Any())
            {
                return NotFound();
            }

            var filteredAuthorBooks = authorsBook.Where(ab => ab.Book != null).ToList();

            return Ok(filteredAuthorBooks);
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutAuthor(int id, Author author)
        //{
        //    if (id != author.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(author).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AuthorExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(CreateAuthorsDto author)
        {
            var publisher = await _context.Authors.AnyAsync(a => a.Id == author.PublisherId);
            if (!publisher)
                return NotFound();

            var newAuthor = new Author
            {
                FirstName = author.FirstName,
                LastName = author.LastName,
                PublisherId = author.PublisherId
            };

            _context.Authors.Add(newAuthor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuthor", new { id = author.Id }, author);
        }

        // DELETE: api/Authors/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAuthor(int id)
        //{
        //    var author = await _context.Authors.FindAsync(id);
        //    if (author == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Authors.Remove(author);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool AuthorExists(int id)
        //{
        //    return _context.Authors.Any(e => e.Id == id);
        //}
    }
}
