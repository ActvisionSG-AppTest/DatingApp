using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Controllers.Resources;
using DatingApp.API.Models;
using DatingApp.API.Persistence.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _Repository;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository Repository, IConfiguration config)
        {
            this._config = config;
            this._Repository = Repository;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AuthResource authResource)
        {

            //validate request -- not required since [ApiController] is specified
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);

            authResource.Username = authResource.Username.ToLower();

            if (await _Repository.IsRegistered(authResource.Username))
                return BadRequest("Username already exists");

            var userToCreate = new User
            {
                Username = authResource.Username
            };

            var createdUser = await _Repository.Register(userToCreate, authResource.Password);

            return StatusCode(201);
        }

        [HttpPost("login")]

        public async Task<IActionResult> Login([FromBody] LoginResource loginResource)
        {
            var userRepo = await _Repository.Login(loginResource.Username.ToLower(), loginResource.Password);

            if (userRepo == null)
            {
                // ModelState.AddModelError("Username", "Invalid username or password");
                // return BadRequest(ModelState);
                return Unauthorized();
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userRepo.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });
        }
    }
}