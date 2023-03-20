using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using task_three.Models;

namespace task_three.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly DataContext _context;

        public PublisherController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Publisher>> Get(int PublisherId)
        {
            var publish = await _context.Publishers
                .Where(context => context.Id == PublisherId)
                .ToListAsync();
            return Ok(publish);
        }

        [HttpGet]
        public async Task<ActionResult<Publisher>> Get()
        {
            return Ok(await _context.Publishers.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<Publisher>> Create(PublishVC models)
        {
            if (models == null)
            {
                return BadRequest();
            }
            var dbpublish = new Publisher
            {
                Name_Of_Publisher = models.Name_Of_Publisher,
                State_Of_Origin = models.State_Of_Origin
            };

            var userPublish = await _context.Publishers.FirstOrDefaultAsync(x => x.Name_Of_Publisher == dbpublish.Name_Of_Publisher);

            if (userPublish != null)
                return BadRequest("Publisher already exist");

            _context.Publishers.Add(dbpublish);
            await _context.SaveChangesAsync();
            return Ok(dbpublish);
        }

        [HttpDelete]
        public async Task<ActionResult<List<Publisher>>> Delete(int id)
        {
            var pubDel = await _context.Publishers.FindAsync(id);
            if (pubDel == null)
                return BadRequest("Author not Found");

            _context.Publishers.Remove(pubDel);
            await _context.SaveChangesAsync();

            return Ok(await _context.Publishers.ToListAsync());
        }
    }
}
