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


        public async Task<Stat> AverageChangeByDateDiff(int difference)
        {
            return new Stat
            {
                Current = (
                    await Context
                    .HourlyTrends
                    .Where(t => t.Date <= DateTime.Now.AddDays(-difference).Date)
                    .AverageAsync(t => t.OverallChange)
                ),
                Values = await Context.HourlyTrends.GroupBy(t => t.Date).Select(group => group.Average(g => g.OverallChange)).ToListAsync(),
                Dates = await Context.HourlyTrends.Select(t => t.Date).Distinct().ToListAsync()
            };
        }
    }


}
