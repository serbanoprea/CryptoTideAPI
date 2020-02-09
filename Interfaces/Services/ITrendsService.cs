using Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interfaces.Services
{
    public interface ITrendsService
    {
        Task<IEnumerable<HourlyTrend>> GetTrends(DateTime from, DateTime to, int limit=20);
    }
}