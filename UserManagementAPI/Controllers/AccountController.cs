using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserManagement.Core.Interfaces.Services;
using UserManagement.Core.Models;

namespace UserManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserService _userService;

        public AccountController(IConfiguration config, IUserService userService)
        {
            _config = config;
            _userService = userService;
        }

        /// <summary>
        /// This API will authenticate the user login and returns the bearer token if successful
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] UserLoginModel model)
        {
            if (string.IsNullOrEmpty(model.UserName))
            {
                return NotFound("UserName is empty");
            }

            if (string.IsNullOrEmpty(model.Password))
            {
                return NotFound("Password is empty");
            }

            //If user is authenticated then generate token
            if (Authenticate(model))
            {
                string token = GenerateToken(model);
                return Ok(new { Token = token });
            }

            return NotFound("Username or Password is not correct");
        }

        /// <summary>
        /// This method will check if a user exists with given credentials
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        private bool Authenticate(UserLoginModel userLogin)
        {
            return _userService.AuthenticateUser(userLogin);
        }

        /// <summary>
        /// This method will generate JWT token
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        private string GenerateToken(UserLoginModel userLogin)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userLogin.UserName)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
