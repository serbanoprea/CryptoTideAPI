using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Models.Authentication;
using Models.DatabaseModels;

namespace CryptoTideAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ITrendsService service;
        public TestController(ITrendsService services)
        {
            service = services;
        }

        [HttpGet]
        public async Task<IEnumerable<HourlyTrend>> Get()
        {
            var d1 = DateTime.Now;
            var d2 = DateTime.Now.Subtract(new TimeSpan(7, 0, 0, 0));
            var coins = await service.GetTrends(d2, d1);
            return coins;
        }
    }
}