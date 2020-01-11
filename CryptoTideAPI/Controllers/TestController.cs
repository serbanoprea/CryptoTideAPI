using System.Collections.Generic;
using System.Threading.Tasks;
using Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Models.Authentication;

namespace CryptoTideAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IUsersService service;
        public TestController(IUsersService services)
        {
            service = services;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            var coins = await service.GetAllUsers();
            return coins;
        }
    }
}