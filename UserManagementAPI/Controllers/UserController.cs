using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Core.Interfaces.Services;
using UserManagement.Core.Models;

namespace UserManagementAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
                
        [HttpGet]
        [Route("Get")]
        public IActionResult GetUser()
        {
            try
            {
                var users = _userService.GetUser();
                return Ok(new { users });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult AddUser(User user)
        {
            try
            {
                _userService.AddUser(user);
                return Ok(new { status = true });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        [Route("Edit")]
        public IActionResult EditUser(User user)
        {
            try
            {
                _userService.EditUser(user);
                return Ok(new { status = true });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("Delete/{Id}")]
        public IActionResult DeleteUser(int Id)
        {
            try
            {
                _userService.DeleteUser(Id);
                return Ok(new { status = true });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
