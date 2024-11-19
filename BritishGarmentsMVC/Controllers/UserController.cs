using BritishGarmentsMVC.Models;
using BritishGarmentsMVC.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BritishGarmentsMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {

        private readonly IUserService _userService = userService;

        // GET: api/users
        [HttpGet("GetAll")]
        public IActionResult GetUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

    }
}
