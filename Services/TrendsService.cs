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
        public TrendsService(IConfiguration config)
        {
            Context = new CryptoTideDBContext(config);
        }

        public async Task<IEnumerable<HourlyTrend>> GetTrends(DateTime from, DateTime to, int limit=20)
        {
            var trends = await Context
                .HourlyTrends
                .Where(t => t.Date > from && t.Date < to)
                .OrderBy(t => t.PercentageIncrease / t.ConsecutiveIncreases)
                .Take(limit)
                .ToListAsync();

            return trends;
        }

        public async Task<IEnumerable<HourlyTrend>> GetTrendBySymbol(string symbol)
        {
            var trends = await Context.HourlyTrends.Where(t => t.Symbol.Equals(symbol, StringComparison.InvariantCultureIgnoreCase)).ToListAsync();
            return trends;
        }
    }
}
