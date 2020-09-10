using Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interfaces.Services
{
    public interface IPopulationStatsService
    {
        Task<IEnumerable<CoinAggregateDTO>> DailyBestPerformers();
        Task<CoinAggregateDTO> SingleCoinAggregate(string symbol);
    }
}