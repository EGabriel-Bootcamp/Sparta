using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User_Management.Domain;
using User_Management.Repository;

namespace User_Management.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository<User> _userRepository;

        public UserController(IUserRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser([FromBody] User user)
        {
            var newUser = await _userRepository.Create(user);
            return Ok(newUser);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userRepository.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser(int id, [FromBody] User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            var existingUser = await _userRepository.GetById(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            var updatedUser = await _userRepository.Update(user);
            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userRepository.Delete(user);
            return NoContent();
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<User>>> FilterUsers(int? age, string gender, string maritalStatus, string residence)
        {
            var users = await _userRepository.GetAll();

            if (age.HasValue)
            {
                users = users.Where(u => u.Age == age.Value);
            }

            if (!string.IsNullOrEmpty(gender))
            {
                users = users.Where(u => u.Gender == gender);
            }

            if (!string.IsNullOrEmpty(maritalStatus))
            {
                users = users.Where(u => u.MaritalStatus == maritalStatus);
            }

            if (!string.IsNullOrEmpty(residence))
            {
                users = users.Where(u => u.Residence == residence);
            }

            return Ok(users);

        }
    }
}


