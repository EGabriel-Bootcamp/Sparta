using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using UserSecurityDomain.Entities;
using UserSecurityDomain.Model.Dtos;

namespace UserCachingAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static AuthUser authUser = new AuthUser();
        private readonly IConfiguration configuration;

        public AuthController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthUser>> Register(AuthUserDto request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            authUser.Username = request.Username;
            authUser.PasswordHash = passwordHash;
            authUser.PasswordSalt = passwordSalt;

            return Ok(authUser);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(AuthUserDto request)
        {
            if (authUser.Username != request.Username)
            {
                return BadRequest("Invalid User");
            }

            if (!VerifyPasswordHash(request.Password, authUser.PasswordHash, authUser.PasswordSalt))
            {
                return BadRequest("Wrong Password");
            }

            string token = CreateToken(authUser);

            //var refreshToken = GeneratefreshToken();
            //SetfreshToken(refreshToken);
            return Ok(token);
        }

        private string CreateToken(AuthUser user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.
                GetBytes(configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }

}
