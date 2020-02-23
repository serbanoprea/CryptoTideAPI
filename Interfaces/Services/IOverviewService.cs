using Models.DatabaseModels;
using Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interfaces.Services
{
    public interface IOverviewService
    {
        Task<IEnumerable<OverviewDTO>> GetFirstCoins(int limit = 20);        
        OverviewDTO GetCoinBySymbol(string symbol);
    }
}