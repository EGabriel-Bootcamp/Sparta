using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using User_Management.DataAccess.User_Context;
using User_Management.Domain.Entities;
using User_Management.Domain.Repository;
using User_Management.Domain.ViewModel;

namespace User_Management_App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IUserRepository _userRepository;

        public UserController(IUnitOfWork UnitOfWork, IUserRepository userRepository)
        {
            _UnitOfWork = UnitOfWork;
            _userRepository = userRepository;
        }

        [HttpGet("Get_All_Users")]
        public ActionResult Get()
        {
            var userfromRepos = _userRepository.GetAll();
            return Ok(userfromRepos);
        }

        [HttpGet("Get_One_Users")]
        public ActionResult Get(int id) 
        {
             var userIdRepo = _userRepository.GetById(id);
            return Ok(userIdRepo);

        }

        [HttpPost ("Create_Users")]
        public  ActionResult Create(User user) 
        {
             _userRepository.Add(user);
            _UnitOfWork.Save();
            return Ok(user);
        }
        [HttpDelete ("Delete_User")]
        public ActionResult Delete(int id)
        {
            var delUser = _userRepository.GetById(id);
            _userRepository.Remove(delUser);
            _UnitOfWork.Save();
            return Ok(delUser);
        }

        [HttpPut("Edit_User")]
        public ActionResult Put(User request)
        {
            var edUser = _userRepository.GetById(request.Id);
            if (edUser == null)
            {
                return BadRequest();
            }
            edUser.FirstName = request.FirstName;
            edUser.LastName = request.LastName;
            edUser.Age = request.Age;
            edUser.StateOfResident = request.StateOfResident;
            edUser.MartialStatus = request.MartialStatus;

            

            _userRepository.updateUser(edUser);
            _UnitOfWork.Save();
            return Ok(edUser);
        }

        //Filtering

        [HttpGet("Get_Users_Gender_martialStatus_StateOf_Resident")]
        public IActionResult GetUsersWithFilter([FromQuery]UserFilter model)
        {

            int age = 0;
            int modelCheck = default;
            var user = _userRepository.GetAll().Select(x => new UserViewModel 
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Age = x.Age,
                Gender = x.Gender,
                MartialStatus = x.MartialStatus,
                StateOfResident = x.StateOfResident
            });

            if (model.Filter != null)
            {
                switch (model.Filter)
                {
                    case "MaritalStatus":
                        user = user.Where(x => x.MartialStatus.ToLower() == model.Keyword.ToLower());
                        break;

                    case "Gender":
                        user = user.Where(x => x.Gender.ToLower() == model.Keyword.ToLower());
                        break;

                    case "StateOfResident":
                        user = user.Where(x => x.StateOfResident.ToLower() == model.Keyword.ToLower());
                        break;

                    case "Age":
                        var check = int.TryParse(model.Keyword, out modelCheck);
                        if (check == true)
                        {
                            age = Convert.ToInt32(model.Keyword);
                            user = user.Where(x => x.Age == age);
                        }
                        break;
                    default:

                        break;
                }
            }

            return Ok(user);

        }
    }
}
