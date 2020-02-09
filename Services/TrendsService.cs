using DataAccess.DatabaseAccess;
using Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class TrendsService : ITrendsService
    {
        private readonly CryptoTideDBContext Context;
        private readonly int LastHour;

        public TrendsService(IConfiguration config)
        {
            Context = new CryptoTideDBContext(config);

            // TODO(Serban): Hacky, will need to keep value in memory and be updated by the data pipeline
            LastHour = Context.HourlyTrends.Where(t => t.Date == DateTime.Now.Date).Max(t => t.Hour);
        }

        public async Task<IEnumerable<HourlyTrend>> GetTrends(DateTime from, DateTime to, int limit=20)
        {
            var trends = await Context
                .HourlyTrends
                .Where(t => t.Date == DateTime.Now.Date && t.Hour == LastHour)
                .OrderBy(t => t.PercentageIncrease * t.ConsecutiveIncreases)
                .Take(limit)
                .ToListAsync();

            return trends;
        }

        public async Task<IEnumerable<HourlyTrend>> GetTrendBySymbol(string symbol)
        {
            var trends = await Context
                .HourlyTrends
                .Where(t => t.Symbol.Equals(symbol, StringComparison.InvariantCultureIgnoreCase))
                .ToListAsync();

            return trends;
        }
    }
}
