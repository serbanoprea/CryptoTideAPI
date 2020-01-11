using DataAccess.DatabaseAccess;
using Interfaces.UnitOfWork.IDBUnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace UnitsOfWork.DatabaseUnitOfWork
{
    public class DBUnitOfWork : IDBUnitOfWork
    {
        private readonly CryptoTideDBContext context;

        public DBUnitOfWork(IConfiguration config)
        {
            context = new CryptoTideDBContext(config);
        }

        public void Create<T>(T entity) where T : class
        {
            var dbEntry = context.Entry(entity);
            if (dbEntry.State != EntityState.Detached)
                dbEntry.State = EntityState.Added;
            else
                context.Set<T>().Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete<T>(T entity) where T : class
        {
            context.Entry(entity).State = EntityState.Deleted;
        }

        public async Task CommitChanges()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
