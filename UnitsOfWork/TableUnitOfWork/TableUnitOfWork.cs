using DataAccess.TableAccess;
using Interfaces.ITableUnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage.Table;
using System.Threading.Tasks;

namespace UnitsOfWork.TableUnitOfWork
{
    public class TableUnitOfWork : ITableUnitOfWork
    {
        private CloudTable context;
        private TableBatchOperation operations;
        private IConfiguration configuration;
        
        public TableUnitOfWork(IConfiguration config)
        {
            operations = new TableBatchOperation();
            configuration = config;
        }

        public void SetTable(string table)
        {
            context = new CryptoTideTableContext(configuration, table).Table;
        }

        public void Create<T>(T entity) where T : TableEntity
        {
            var createOperation = TableOperation.Insert(entity);
            operations.Add(createOperation);
        }

        public void Update<T>(T entity) where T : TableEntity
        {
            var updateOperation = TableOperation.Replace(entity);
            operations.Add(updateOperation);
        }

        public void Delete<T>(T entity) where T : TableEntity
        {
            var deleteOperation = TableOperation.Delete(entity);
            operations.Add(deleteOperation);
        }

        public async Task CommitChanges()
        {
            await context.ExecuteBatchAsync(operations);
            operations = new TableBatchOperation();
        }
    }
}
