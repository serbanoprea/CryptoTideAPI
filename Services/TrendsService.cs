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
            try
            {
                // Errors if max is 0
                LastHour = Context.HourlyTrends.Where(t => t.Date == DateTime.Now.Date).Max(t => t.Hour);
            }
            catch
            {
                LastHour = 0;
            }
        }

        public async Task<IEnumerable<HourlyTrendDTO>> GetHourlyTrends(int limit=20)
        {
            var trends = await Context
                .HourlyTrends
                .Where(t => t.Date == DateTime.Now.Date && t.Hour == LastHour)
                .OrderByDescending(t => t.PercentageIncrease * t.ConsecutiveIncreases)
                .Take(limit)
                .ToListAsync();

            return await ModelToDTOs(trends);
        }

        public async Task<IEnumerable<HourlyTrendDTO>> GetTrendBySymbol(string symbol)
        {
            var trends = await Context
                .HourlyTrends
                .Where(t => t.Symbol == symbol)
                .ToListAsync();

            return await ModelToDTOs(trends);
        }

        public async Task<IEnumerable<DailyTrendDTO>> GetDailyTrends(int limit=20)
        {
            var coins = await IngressService.GetAllCoins();
            var trends = await Context
                .DailyTrends
                .Where(t => t.Date == DateTime.Today.AddDays(-1))
                .OrderByDescending(t => t.ConsecutiveHighChange * t.HighPercChange)
                .Take(limit)
                .ToListAsync();

            return ModelToDTOs(trends, coins);
        }

        public async Task<HourlyTrendsGraphDTO> HourlyTrendGraph(string symbol)
        {
            var trends = await Context.HourlyTrends.Where(s => s.Symbol == symbol).ToListAsync();
            var coins = await IngressService.GetAllCoins();
            return trends
                .GroupBy(t => t.Symbol)
                .Select(group => GroupToDTO(group, coins))
                .First();
        }

        public async Task<IEnumerable<HourlyTrendsGraphDTO>> HourlyTrendsGraph()
        {
            var trends = (await GetHourlyTrends()).Select(t => $"'{t.Symbol}'");
            var coins = await IngressService.GetAllCoins();
            var filteredTrends = await Context
                    .HourlyTrends
                    .FromSqlRaw($"SELECT * FROM HourlyTrends WHERE Symbol IN ({string.Join(",", trends)})")
                    .ToListAsync();

            return filteredTrends
                    .GroupBy(t => t.Symbol)
                    .Select(group => GroupToDTO(group, coins));
        }

        // Should be in helpers
        public HourlyTrendsGraphDTO GroupToDTO(IGrouping<string, HourlyTrend> group, IEnumerable<Coins> coins)
        {
            var groupList = group.ToList();
            return new HourlyTrendsGraphDTO
            {
                Name = coins.Single(c => c.Symbol.Equals(group.Key)).Name,
                Dates = groupList.Select(t => t.Date.AddHours(t.Hour)),
                Changes = groupList.Select(t => t.OverallChange),
                Prices = groupList.Select(t => t.Price),
                ConsecutiveIncreasePerc = groupList.Select(t => t.PercentageIncrease),
                MaxConsecutiveIncreases = groupList.Max(t => t.ConsecutiveIncreases),
                MaxIncreaseSeries = groupList.Max(t => t.PercentageIncrease)
            };
        }

        public async Task<IEnumerable<HourlyTrendsGraphDTO>> GetFirstCoinsTrends(int limit = 20)
        {
            var topCoins = await Context
                .Coins
                .Where(c => c.Rank <= limit)
                .ToListAsync();

            var trends = await Context
                    .HourlyTrends
                    .FromSqlRaw($"SELECT * FROM HourlyTrends WHERE Symbol IN ({string.Join(",", topCoins.Select(t => $"'{t.Symbol}'"))})")
                    .ToListAsync();

            return trends.GroupBy(t => t.Symbol).Select(group => GroupToDTO(group, topCoins));
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

        private IEnumerable<DailyTrendDTO> ModelToDTOs(IEnumerable<DailyTrend> trends, IEnumerable<Coins> coins)
        {
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
