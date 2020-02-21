using Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interfaces.Services
{
    public interface ITrendsService
    {
        Task<IEnumerable<HourlyTrendDTO>> GetHourlyTrends(int limit = 20);
        Task<IEnumerable<HourlyTrendDTO>> GetTrendBySymbol(string symbol);
        Task<IEnumerable<DailyTrendDTO>> GetDailyTrends(int limit = 20);
        Task<IEnumerable<HourlyTrendsGraphDTO>> HourlyTrendsGraph();
        Task<HourlyTrendsGraphDTO> HourlyTrendGraph(string symbol);
    }
}
