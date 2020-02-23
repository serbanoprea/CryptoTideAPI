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
            if (trends == null)
                return NotFound();

            return Ok(trends);
        }
        
        [HttpGet]
        [Route("LatestDailyTrends")]
        public async Task<IActionResult> GetDailyTrends()
        {
            var trends = await service.GetDailyTrends();
            return Ok(trends);
        }

        [HttpGet]
        [Route("GraphHourlyTrends/{symbol}")]
        public async Task<IActionResult> GetHourlyGraphTrends(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                return BadRequest();

            var graphTrends = await service.HourlyTrendGraph(symbol);

            if (graphTrends == null)
                return NotFound();

            return Ok(graphTrends);
        }

        [HttpGet]
        [Route("GraphHourlyTrends")]
        public async Task<IActionResult> GetHourlyGraphTrends()
        {
            var graphTrends = await service.HourlyTrendsGraph();
            return Ok(graphTrends);
        }

        [HttpGet]
        [Route("GraphTopRanked")]
        public async Task<IActionResult> GetGraphRanked()
        {
            var graphTopRanked = await service.GetFirstCoinsTrends();
            return Ok(graphTopRanked);
        }
    }
}