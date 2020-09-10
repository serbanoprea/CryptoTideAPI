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
        private readonly int LastHour;

        public PopulationStatsService(IConfiguration config)
        {
            Context = new CryptoTideDBContext(config);

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
        
        public async Task<IEnumerable<CoinAggregateDTO>> DailyBestPerformers()
        {
            var aggregates = await Context.CoinAggregates.OrderByDescending(c => c.DayChange).Take(5).ToListAsync();
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
                Price = aggregate.Price,
                DayChange = aggregate.DayChange,
                WeekChange = aggregate.WeekChange,
                MonthChange = aggregate.MonthChange,

                Min24hVolatility = aggregate.Min24hVolatility,
                Small24hVolatility = aggregate.Small24hVolatility,
                Medium24hVolatility = aggregate.Medium24hVolatility,

                Min24hChange = aggregate.Min24hChange,
                High24hVolatility = aggregate.High24hVolatility,
                Max24hVolatility = aggregate.Max24hVolatility,
                Small24hChange = aggregate.Small24hChange,
                Medium24hChange = aggregate.Medium24hChange,
                High24hChange = aggregate.High24hChange,
                Max24hChange = aggregate.Max24hChange,

                MinWeekVolatility = aggregate.MinWeekVolatility,
                SmallWeekVolatility = aggregate.SmallWeekVolatility,
                MediumWeekVolatility = aggregate.MediumWeekVolatility,
                HighWeekVolatility = aggregate.HighWeekVolatility,
                MaxWeekVolatility = aggregate.MaxWeekVolatility,

                MinWeekChange = aggregate.MinWeekChange,
                SmallWeekChange = aggregate.SmallWeekChange,
                MediumWeekChange = aggregate.MediumWeekChange,
                HighWeekChange = aggregate.HighWeekChange,
                MaxWeekChange = aggregate.MaxWeekChange,

                MinMonthChange = aggregate.MinMonthChange,
                SmallMonthChange = aggregate.SmallMonthChange,
                MediumMonthChange = aggregate.MediumMonthChange,
                HighMonthChange = aggregate.HighMonthChange,
                MaxMonthChange = aggregate.MaxMonthChange,

                MinMonthVolatility = aggregate.MinMonthVolatility,
                SmallMonthVolatility = aggregate.SmallMonthVolatility,
                MediumMonthVolatility = aggregate.MediumMonthVolatility,
                HighMonthVolatility = aggregate.HighMonthVolatility,
                MaxMonthVolatility = aggregate.MaxMonthVolatility
            };
        }
    }
}
