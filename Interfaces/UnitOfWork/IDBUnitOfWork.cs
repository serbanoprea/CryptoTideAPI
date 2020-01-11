using System.Threading.Tasks;

namespace Interfaces.UnitOfWork.IDBUnitOfWork
{
    public interface IDBUnitOfWork
    {
        Task CommitChanges();
        void Create<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void Dispose();
        void Update<T>(T entity) where T : class;
    }
}