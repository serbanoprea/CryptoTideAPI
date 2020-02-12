﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Models.DatabaseModels;
using Models.DTOs;

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
        public async Task<IEnumerable<HourlyTrendDTO>> Get()
        {
            var coins = await service.GetHourlyTrends();
            return coins;
        }
    }
}