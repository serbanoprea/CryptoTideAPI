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
    public class OverviewController : ControllerBase
    {
        private readonly IOverviewService Service;
        public OverviewController(IOverviewService service)
        {
            Service = service;
        }

        [HttpGet]
        [Route("TopOverview")]
        public async Task<IActionResult> GetTopOverview()
        {
            var values = await Service.GetFirstCoins();
            return Ok(values);
        }

        [HttpGet]
        [Route("CoinOverview/{symbol}")]
        public IActionResult GetOverviewBySymbol(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                return BadRequest();

            var coin = Service.GetCoinBySymbol(symbol);

            if (coin == null)
                return NotFound();

            return Ok(coin);
        }
    }
}