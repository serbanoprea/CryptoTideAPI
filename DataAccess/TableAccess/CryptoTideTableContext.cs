using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;

namespace DataAccess.TableAccess
{
    public class CryptoTideTableContext
    {
        public readonly CloudTable Table;

        public CryptoTideTableContext(IConfiguration configuration, string table)
        {
            var configOptions = configuration.GetSection("Azure");
            var name = configOptions.GetSection("StorageAccountName").Value;
            var key = configOptions.GetSection("Key").Value;

            StorageCredentials credentials = new StorageCredentials(name, key);
            CloudStorageAccount account = new CloudStorageAccount(credentials, true);

            var tableClient = account.CreateCloudTableClient();           
            Table = tableClient.GetTableReference(table);
        }
    }
}
