using Models.DatabaseModels;
using Models.DTOs;
using System.Collections.Generic;
using System.Linq;
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
        HourlyTrendsGraphDTO GroupToDTO(IGrouping<string, HourlyTrend> group, IEnumerable<Coins> coins);
        Task<IEnumerable<HourlyTrendsGraphDTO>> GetFirstCoinsTrends(int limit = 20);
    }
}
