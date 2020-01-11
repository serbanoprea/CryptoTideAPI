using Microsoft.WindowsAzure.Storage.Table;
using System.Threading.Tasks;

namespace Interfaces.ITableUnitOfWork
{
    public interface ITableUnitOfWork
    {
        Task CommitChanges();
        void Create<T>(T entity) where T : TableEntity;
        void Delete<T>(T entity) where T : TableEntity;
        void Update<T>(T entity) where T : TableEntity;
        void SetTable(string table);
    }
}