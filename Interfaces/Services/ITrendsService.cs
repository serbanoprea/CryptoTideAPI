using Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interfaces.Services
{
    public interface ITrendsService
    {
        Task<IEnumerable<HourlyTrend>> GetTrends(int limit=20);
        Task<IEnumerable<HourlyTrend>> GetTrendBySymbol(string symbol);
    }
}