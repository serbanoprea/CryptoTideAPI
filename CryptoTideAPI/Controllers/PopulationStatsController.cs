using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptoTideAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PopulationStatsController : ControllerBase
    {
        private readonly IPopulationStatsService PopulationStatsService;
        public PopulationStatsController(IPopulationStatsService populationStatsService)
        {
            PopulationStatsService = populationStatsService;
        }

        [HttpGet]
        [Route("TopCoinAggregates")]
        public async Task<IActionResult> CoinAggregates()
        {
            var stat = await PopulationStatsService.DailyBestPerformers();
            return Ok(stat);
        }

        [HttpGet]
        [Route("CoinAggregate/{symbol}")]
        public async Task<IActionResult> SingleCoinAggregate(string symbol)
        {
            var stat = await PopulationStatsService.SingleCoinAggregate(symbol);

            if (stat == null)
                return NotFound();

            return Ok(stat);
        }
    }
}