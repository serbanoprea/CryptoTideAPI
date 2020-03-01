using DataAccess.DatabaseAccess;
using Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models.DatabaseModels;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PopulationStatsService : IPopulationStatsService
    {
        private readonly CryptoTideDBContext Context;

        public PopulationStatsService(IConfiguration config)
        {
            Context = new CryptoTideDBContext(config);
        }


        public async Task<IEnumerable<CoinAggregateDTO>> DailyBestPerformers()
        {
            var aggregates = await Context.CoinAggregates.OrderByDescending(c => c.SumDayChange * c.DayRecords).Take(3).ToListAsync();
            var coins = await Context.Coins.ToListAsync();

            return aggregates.Select(aggregate => ModelToDTO(aggregate, coins));
        }

        public async Task<CoinAggregateDTO> SingleCoinAggregate(string symbol)
        {
            var aggregate = await Context.CoinAggregates.SingleOrDefaultAsync(c => c.Symbol == symbol);
            if (aggregate == null)
                return null;
            var coins = await Context.Coins.ToListAsync();
            return ModelToDTO(aggregate, coins);
        }

        public CoinAggregateDTO ModelToDTO(CoinAggregate aggregate, IEnumerable<Coins> coins)
        {
            return new CoinAggregateDTO
            {
                Name = coins.Single(c => c.Symbol == aggregate.Symbol).Name,
                Symbol = aggregate.Symbol,
                AverageDayChange = aggregate.AverageDayChange,
                AverageWeekChange = aggregate.AverageWeekChange,
                StDevDayChange = aggregate.StDevDayChange,
                StDevWeekChange = aggregate.StDevWeekChange,
                SumDayChange = aggregate.SumDayChange,
                SumWeekChange = aggregate.SumWeekChange,

                DayRecords = aggregate.DayRecords,
                WeekRecords = aggregate.WeekRecords,
                DayVolatility = aggregate.DayVolatility,
                WeekVolatility = aggregate.WeekVolatility,

                HigherThan25PercDay = aggregate.HigherThan25PercDay,
                HigherThan75PercDay = aggregate.HigherThan75PercDay,
                HigherThanMedianDay = aggregate.HigherThanMedianDay,
                HigherThanAverageDay = aggregate.HigherThanAverageDay,

                HigherThan25PercWeek = aggregate.HigherThan25PercWeek,
                HigherThanMedianWeek = aggregate.HigherThanMedianWeek,
                HigherThanAverageWeek = aggregate.HigherThanAverageWeek,
                HigherThan75PercWeek = aggregate.HigherThan75PercWeek,

                HigherThanAverageVolatilityDay = aggregate.HigherThanAverageVolatilityDay,
                HigherVolatilityThan25PercDay = aggregate.HigherVolatilityThan25PercDay,
                HigherVolatilityThanMedianDay = aggregate.HigherVolatilityThanMedianDay,
                HigherVolatilityThan75PercDay = aggregate.HigherVolatilityThan75PercDay,
                MaxDayVolatility = aggregate.MaxDayVolatility,
                MinDayVolatility = aggregate.MinDayVolatility,
                
                HigherVolatilityThan25PercWeek = aggregate.HigherVolatilityThan25PercWeek,
                HigherVolatilityMedianWeek = aggregate.HigherVolatilityMedianWeek,
                HigherThanAverageVolatilityWeek = aggregate.HigherThanAverageVolatilityWeek,
                HigherVolatilityThan75PercWeek = aggregate.HigherVolatilityThan75PercWeek,
                MaxWeekVolatility = aggregate.MaxWeekVolatility,
                MinWeekVolatility = aggregate.MinWeekVolatility
            };
        }
    }
}
