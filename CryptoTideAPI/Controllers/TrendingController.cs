using System.Collections.Generic;
using System.Threading.Tasks;
using Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DatabaseModels;

namespace CryptoTideAPI.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/[controller]")]
    public class TrendingController : ControllerBase
    {
        private readonly ITrendsService service;
        public TrendingController(ITrendsService Service)
        {
            service = Service;
        }

        [HttpGet]
        [Route("LatestHourlyTrends")]
        public async Task<IActionResult> GetHourlyTrends()
        {
            var trends = await service.GetHourlyTrends();

            return Ok(trends);
        }

        [HttpGet]
        [Route("HourlyTrendBySymbol/{symbol}")]
        public async Task<IActionResult> GetHourlyTrendByCoin(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                return BadRequest();

            var trends = await service.GetTrendBySymbol(symbol);

            return Ok(trends);
        }
        
        [HttpGet]
        [Route("LatestDailyTrends")]
        public async Task<IActionResult> GetDailyTrends()
        {
            var trends = await service.GetDailyTrends();

            return Ok(trends);
        }
    }
}