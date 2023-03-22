using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using task_three.Models;

namespace task_three.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly DataContext _context;

        public AuthorController(DataContext context)
        {
            _context = context;
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<Authors>> Get(int AuthorsId)
        {
            var authors = await _context.Authors
                .Where(context => context.Id == AuthorsId)
                //.Include(x => x.Books)
                .ToListAsync();

            return Ok(authors);
        }

        [HttpGet]
        public async Task<ActionResult<List<Authors>>> Get()
        {
           return Ok(await _context.Authors.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<Authors>> Create(AuthorVM model)
        {

            if (model == null)
            {
                return BadRequest();
            }
            var author = new Authors
            {
                Names_Of_Author = model.Names_Of_Author,
                State_Of_Origin = model.State_Of_Origin,
                publisherId = model.publisherId
            };

            var userAuthor = await _context.Publishers.FindAsync(author.publisherId);
            if(userAuthor == null)
                return NotFound();

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return await Get(author.Id);
        }

        [HttpDelete]
        public async Task<ActionResult<List<Authors>>> Delete(int id)
        {
            var del = await _context.Authors.FindAsync(id);
            if(del == null)
                return BadRequest("Author not found.");

            _context.Authors.Remove(del);
            await _context.SaveChangesAsync();

            return Ok(await _context.Authors.ToListAsync());
        }
    }
}
