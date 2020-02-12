using DataAccess.DatabaseAccess;
using Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models.DatabaseModels;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class TrendsService : ITrendsService
    {
        private readonly CryptoTideDBContext Context;
        private readonly IIngressService IngressService;
        private readonly int LastHour;

        public TrendsService(IConfiguration config, IIngressService ingressService)
        {
            Context = new CryptoTideDBContext(config);
            IngressService = ingressService;

            // TODO(Serban): Hacky, will need to keep value in memory and be updated by the data pipeline
            LastHour = Context.HourlyTrends.Where(t => t.Date == DateTime.Now.Date).Max(t => t.Hour);
        }

        public async Task<IEnumerable<HourlyTrendDTO>> GetHourlyTrends(int limit=20)
        {
            var trends = await Context
                .HourlyTrends
                .Where(t => t.Date == DateTime.Now.Date && t.Hour == LastHour)
                .OrderByDescending(t => t.PercentageIncrease * t.ConsecutiveIncreases)
                .Take(limit)
                .ToListAsync();

            var dtos = await ModelToDTOs(trends);

            return dtos;
        }

        public async Task<IEnumerable<HourlyTrendDTO>> GetTrendBySymbol(string symbol)
        {
            var trends = await Context
                .HourlyTrends
                .Where(t => t.Symbol == symbol)
                .ToListAsync();

            var dtos = await ModelToDTOs(trends);

            return dtos;
        }

        public async Task<IEnumerable<DailyTrendDTO>> GetDailyTrends(int limit=20)
        {
            var trends = await Context
                .DailyTrends
                .Where(t => t.Date == DateTime.Today.AddDays(-1))
                .OrderByDescending(t => t.ConsecutiveHighChange * t.HighPercChange)
                .Take(limit)
                .ToListAsync();

            var dtos = await ModelToDTOs(trends);

            return dtos;
        }

        private async Task<IEnumerable<HourlyTrendDTO>> ModelToDTOs(IEnumerable<HourlyTrend> trends)
        {
            var coins = await IngressService.GetAllCoins();

            return trends.Select(t => new HourlyTrendDTO
            {
                Name = coins.Single(c => c.Symbol.Equals(t.Symbol)).Name,
                Symbol = t.Symbol,
                Date = t.Date,
                Hour = t.Hour,
                Price = t.Price,
                ConsecutiveIncreases = t.ConsecutiveIncreases,
                PercentageIncrease = t.PercentageIncrease,
                OverallChange = t.OverallChange
            });
        }

        private async Task<IEnumerable<DailyTrendDTO>> ModelToDTOs(IEnumerable<DailyTrend> trends)
        {
            var coins = await IngressService.GetAllCoins();

            return trends.Select(t => new DailyTrendDTO
            {
                Name = coins.Single(c => c.Symbol.Equals(t.Symbol)).Name,
                Symbol = t.Symbol,
                Date = t.Date,
                MinPrice = t.MinPrice,
                Price25Perc = t.Price25Perc,
                PriceMedian = t.PriceMedian,
                Price75Perc = t.Price75Perc,
                MaxPrice = t.MaxPrice,
                OverallChange = t.OverallChange,
                SmallPercChange = t.SmallPercChange,
                MediumPercChange = t.MediumPercChange,
                HighPercChange = t.HighPercChange,
                MedianChange = t.MedianChange,
                Median75PercChange = t.Median75PercChange,
                MedianMaxChange = t.MedianMaxChange,
                ConsecutiveSmallChange = t.ConsecutiveSmallChange,
                ConsecutiveMediumChange = t.ConsecutiveMediumChange,
                ConsecutiveHighChange = t.ConsecutiveHighChange
            });
        }
    }
}
