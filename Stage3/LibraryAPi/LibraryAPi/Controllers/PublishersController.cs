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
    public class PublishersController : ControllerBase
    {
        private readonly LibraryContext _context;

        public PublishersController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/Publishers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Publisher>>> GetPublishers()
        {
            return await _context.Publishers.ToListAsync();
        }

        // GET: api/Publishers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Publisher>> GetPublisher(int id)
        {
            var publisher = await _context.Publishers.FindAsync(id);

            if (publisher == null)
            {
                return NotFound();
            }

            return publisher;
        }

        // GET: api/PublishersAuthors/5
        [HttpGet("{id}/PublishersAuthors")]
        public async Task<ActionResult<dynamic>> GetPublishersAuthors(int id)
        {
            var publisher = await _context.Authors
                .Where(b => b.PublisherId == id)
                .ToListAsync();

            if (publisher == null)
            {
                return NotFound();
            }

            return publisher;
        }

        // PUT: api/Publishers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutPublisher(int id, Publisher publisher)
        //{
        //    if (id != publisher.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(publisher).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!PublisherExists(id))
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

        // POST: api/Publishers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Publisher>> PostPublisher(CreatePublisherDto publisher)
        {
            var newPublisher = new Publisher
            {
                Name = publisher.Name
            };
            _context.Publishers.Add(newPublisher);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPublisher", new { id = publisher.Id }, publisher);
        }

        // DELETE: api/Publishers/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeletePublisher(int id)
        //{
        //    var publisher = await _context.Publishers.FindAsync(id);
        //    if (publisher == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Publishers.Remove(publisher);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool PublisherExists(int id)
        //{
        //    return _context.Publishers.Any(e => e.Id == id);
        //}
    }
}
