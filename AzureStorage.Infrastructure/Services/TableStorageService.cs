namespace AzureStorage.Infrastructure.Services
{
    using AzureStorage.Application.Contract;
    using AzureStorage.Domain.Common.Constants;
    using AzureStorage.Domain.Dtos;
    using Microsoft.Azure.Cosmos.Table;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class TableStorageService<T> : ITableStorageService<T> where T : ITableEntity, new()
    {
        #region Properties
        private readonly CloudTableClient _client;
        private readonly ConcurrentDictionary<string, CloudTable> _tables;
        private readonly IConfiguration _configuration;
        private AzureStorageDto? _connectionDto;
        #endregion

        /// <summary>
        /// Initializes a new instance of the AzureTableStorage.
        /// </summary>
        /// /// <param name="configuration">The IConfiguration.</param>
        public TableStorageService(IConfiguration configuration)
        {
            _configuration = configuration;
            GetConfiguration();
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(_connectionDto?.ConnectionString);
            _client = cloudStorageAccount.CreateCloudTableClient();
            _tables = new ConcurrentDictionary<string, CloudTable>();            
        }

        #region Queries
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            CloudTable table = GetTable(typeof(T).Name);
            IEnumerable<T> entities = table.CreateQuery<T>().ToList();

            return await Task.FromResult(entities);
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression)
        {
            CloudTable table = GetTable(typeof(T).Name);
            IEnumerable<T> entities = table.CreateQuery<T>().Where(expression).ToList();

            return await Task.FromResult(entities);
        }

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            CloudTable table = GetTable(typeof(T).Name);
            T? entity = table.CreateQuery<T>().Where(expression).FirstOrDefault();

            return await Task.FromResult(entity);
        }
        #endregion

        #region Commands
        public async Task<string> InsertAsync(T entity)
        {
            CloudTable table = GetTable(typeof(T).Name);
            TableOperation tableOperation = TableOperation.Insert(entity);
            TableResult tableResult = await table.ExecuteAsync(tableOperation);

            return tableResult.Etag;
        }

        public async Task<string> UpdateAsync(T entity)
        {
            CloudTable table = GetTable(typeof(T).Name);
            TableOperation tableOperation = TableOperation.Replace(entity);
            TableResult tableResult = await table.ExecuteAsync(tableOperation);

            return tableResult.Etag;
        }

        public async Task<string> DeleteAsync(T entity)
        {
            CloudTable table = GetTable(typeof(T).Name);
            TableOperation tableOperation = TableOperation.Delete(entity);
            TableResult tableResult = await table.ExecuteAsync(tableOperation);

            return tableResult.Etag;
        }
        #endregion

        #region PrivateMethod
        private void GetConfiguration()
        {
            AzureStorageDto instance = _connectionDto = new AzureStorageDto();
            _configuration.Bind(AzureConstants.AzureStorage, instance);
        }

        private CloudTable GetTable(string tableName)
        {
            if (!_tables.ContainsKey(tableName))
            {
                CloudTable table = _client.GetTableReference(tableName);
                table.CreateIfNotExistsAsync();
                _tables[tableName] = table;
            }

            return _tables[tableName];
        }
        #endregion
    }
}