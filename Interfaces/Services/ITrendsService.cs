using Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interfaces.Services
{
    public interface ITrendsService
    {
        Task<IEnumerable<HourlyTrend>> GetHourlyTrends(int limit=20);
        Task<IEnumerable<HourlyTrend>> GetTrendBySymbol(string symbol);
        Task<IEnumerable<DailyTrend>> GetDailyTrends(int limit = 20);
    }
}