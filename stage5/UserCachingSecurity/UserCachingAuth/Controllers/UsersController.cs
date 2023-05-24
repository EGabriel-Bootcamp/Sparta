using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using UserSecurityDataAccess.Migrations;
using UserSecurityDomain.Entities;
using UserSecurityDomain.Model;
using UserSecurityDomain.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserCachingAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUnit unit;
        private readonly IUserRepository userRepository;
        private readonly IDistributedCache distributedCache;

        public UsersController(IUnit unit, IUserRepository userRepository, IDistributedCache distributedCache)
        {
            this.unit = unit;
            this.userRepository = userRepository;
            this.distributedCache = distributedCache;
        }
        // GET: api/<UsersController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            var users = userRepository.GetAll();
            return Ok(users);
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = userRepository.GetById(id);
            if (user == null)
            {
                return BadRequest();
            }
            return Ok(user);
        }

        // POST api/<UsersController>
        [HttpPost, Authorize(Roles = "Admin")]
        public IActionResult Post(User newUser)
        {
            if (newUser == null)
            {
                return BadRequest();
            }
            userRepository.Create(newUser);
            unit.Save();
            return Ok(newUser);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}"), Authorize(Roles = "Admin")]
        public IActionResult Put(int id, User updatedUser)
        {
            if (updatedUser == null || id != updatedUser.Id)
            {
                return BadRequest();
            }

            var existingUser = userRepository.GetById(id);
            if (existingUser == null)
            {
                return NotFound();
            }
            userRepository.UpdateUser(updatedUser);
            unit.Save();
            return Ok(updatedUser);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var user = userRepository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            userRepository.Delete(user);
            unit.Dispose();
            var users = userRepository.GetAll();
            return Ok(users);
        }

        [HttpGet("FilterUsers")]
        public IActionResult GetUsersWithFilter([FromQuery] UserFilter model)
        {
            var users = userRepository.GetAll()
                .Select(u => new UserViewModel
                {
                    Id = u.Id,
                    Name = u.Name,
                    Age = u.Age,
                    Gender = u.Gender,
                    MaritalStatus = u.MaritalStatus
                });

            if (model.Filter == "Gender")
            {
                users = users.Where(u => u.Gender.ToLower() == model.Keyword.ToLower());
            }
            else if (model.Filter == "MaritalStatus")
            {
                users = users.Where(u => u.MaritalStatus.ToLower() == model.Keyword.ToLower());
            }
            else if (model.Filter == "Age" && int.TryParse(model.Keyword, out var age))
            {
                users = users.Where(u => u.Age == age);
            }

            return Ok(users);
        }

    }
}
