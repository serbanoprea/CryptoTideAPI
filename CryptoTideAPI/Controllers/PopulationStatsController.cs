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
        [Route("Average/{difference}")]
        public async Task<IActionResult> GetDateDiffAverage(int difference = 7)
        {
            var stat = await PopulationStatsService.AverageChangeByDateDiff(difference);
            return Ok(stat);
        }
    }
}