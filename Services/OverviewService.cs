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
    public class OverviewService : IOverviewService
    {
        private readonly CryptoTideDBContext Context;
        private readonly ITrendsService Service;
        private readonly int LastHour;

        public OverviewService(IConfiguration config, ITrendsService service)
        {
            Context = new CryptoTideDBContext(config);
            Service = service;
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

        public async Task<IEnumerable<OverviewDTO>> GetFirstCoins(int limit = 20)
        {
            return (await Context.Coins
                    .Where(c => c.Rank <= limit)
                    .ToListAsync())
                    .Select(ToDTO);
        }

        public OverviewDTO GetCoinBySymbol(string symbol)
        {
            var coin = Context
                        .Coins
                        .SingleOrDefault(c => c.Symbol == symbol);

            if (coin == null)
                return null;

            return ToDTO(coin);
        }


        private OverviewDTO ToDTO(Coins coin)
        {
            var hourlyTrend = Context.HourlyTrends.SingleOrDefault(t => t.CoinId == coin.Id && t.Date == DateTime.Now.Date && t.Hour == LastHour);
            return new OverviewDTO
            {
                Name = coin.Name,
                Symbol = coin.Symbol,
                RankChange = coin.PreviousRank - coin.Rank,
                Price = hourlyTrend.Price,
                IncreaseHours = hourlyTrend.ConsecutiveIncreases,
                Change = hourlyTrend.OverallChange,
                IncreasePerc = hourlyTrend.PercentageIncrease
            };
        }
    }
}
