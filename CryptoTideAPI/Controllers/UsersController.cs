using System.Collections.Generic;
using System.Threading.Tasks;
using Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Authentication;

namespace CryptoTideAPI.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUsersService Service;

        public UsersController(IUsersService usersService)
        {
            Service = usersService;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]UserLogin user)
        {
            var storedUser = await Service.Authenticate(user);

            if (storedUser == null)
                return BadRequest("Invalid username or password.");

            return Ok(storedUser);
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await Service.GetAllUsers();
            return users;
        }
    }
}
