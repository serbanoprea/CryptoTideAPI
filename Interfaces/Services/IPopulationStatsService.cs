using Models.DTOs;
using System.Threading.Tasks;

namespace Interfaces.Services
{
    public interface IPopulationStatsService
    {
        Task<Stat> AverageChangeByDateDiff(int difference);
    }
}