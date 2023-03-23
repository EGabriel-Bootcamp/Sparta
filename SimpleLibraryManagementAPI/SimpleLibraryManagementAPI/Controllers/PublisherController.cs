using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleLibraryManagementAPI.Data;
using SimpleLibraryManagementAPI.Models;
using SimpleLibraryManagementAPI.Repository;

namespace SimpleLibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IPublisherRepository _publisherRepository;
        private readonly IAuthorRepository _authorRepository;

        public PublisherController(AppDbContext context, IPublisherRepository publisherRepository, IAuthorRepository authorRepository)
        {
            _context = context;
            _publisherRepository = publisherRepository;
            _authorRepository = authorRepository;
        }

        [HttpPost]
        public async Task<ActionResult<Publisher>> CreatePublisher(Publisher publisher)
        {
            var CreatedPublisher = _publisherRepository.Create(publisher);
            if (CreatedPublisher == null) return BadRequest("Invalid Request");
            return CreatedAtAction(nameof(GetPublisher), new { id = CreatedPublisher.Id}, CreatedPublisher);
        }

        [HttpGet("{id}")]
        public ActionResult GetPublisher(int id)
        {
            var publisher = _publisherRepository.GetPublisherById(id);

            if (publisher == null)
            {
                return NotFound();
            }

            return Ok(publisher);
        }

        [HttpGet]
        public IActionResult GetAllPublishers()
        {
            return Ok(_publisherRepository.GetAll());
        }

        [HttpGet("{publisherId}/authors")]
        public IActionResult GetAuthorsAttachedToAPublisher(int publisherId)
        {
            var authors = _authorRepository.GetAuthorsOfPublisher(publisherId);

            if (!authors.Any())
            {
                return NotFound();
            }

            return Ok(authors);
        }
    }
}
