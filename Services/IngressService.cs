using DataAccess.DatabaseAccess;
using Interfaces.Services;
using Interfaces.UnitOfWork.IDBUnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class IngressService : IIngressService
    {
        private readonly CryptoTideDBContext Context;
        private readonly IDBUnitOfWork UnitOfWork;
        public IngressService(IConfiguration config, IDBUnitOfWork uow)
        {
            Context = new CryptoTideDBContext(config);
            UnitOfWork = uow;
        }

        public async Task<IEnumerable<Coins>> GetAllCoins()
        {
            var allCoins = await Context.Coins.ToListAsync();
            return allCoins;
        }

        public async Task<Coins> GetSingleCoin(string identifier)
        {
            if (String.IsNullOrWhiteSpace(identifier))
                throw new ArgumentException("Specify identifier");

            var coin = await Context.Coins.SingleOrDefaultAsync(c => c.Identifier.Equals(identifier, StringComparison.InvariantCultureIgnoreCase));

            if (coin == null)
                throw new ArgumentException("Invalid identifier.");

            return coin;
        }

        public async Task CreateCoin(Coins coin)
        {
            UnitOfWork.Create(coin);
            await UnitOfWork.CommitChanges();
        }

        public async Task UpdateCoin(Coins coin)
        {
            UnitOfWork.Update(coin);
            await UnitOfWork.CommitChanges();
        }

        public async Task DeleteCoin(string identifier)
        {
            var coin = await GetSingleCoin(identifier);
            UnitOfWork.Delete(coin);
        }
    }
}
