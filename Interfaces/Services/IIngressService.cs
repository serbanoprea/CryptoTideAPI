using Models.DatabaseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interfaces.Services
{
    public interface IIngressService
    {
        Task CreateCoin(Coins coin);
        Task DeleteCoin(string identifier);
        Task<IEnumerable<Coins>> GetAllCoins();
        Task<Coins> GetSingleCoin(string identifier);
        Task UpdateCoin(Coins coin);
    }
}