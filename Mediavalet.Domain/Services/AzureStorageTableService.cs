using Mediavalet.Domain.Interfaces;
using MediaValet.Core.Utilities;
using Microsoft.Extensions.Options;
using System;
using Azure.Data.Tables;
using System.Threading.Tasks;
using MediaValet.Core.Entities;
using Azure;
using System.Collections.Generic;

namespace Mediavalet.Domain.Services
{
    public class AzureStorageTableService : IAzureTableStorage
    {
        private AzureConfig _azureConfig;
        private TableClient _tableClient;         

        public AzureStorageTableService(IOptionsSnapshot<AzureConfig> options)
        {
            _azureConfig = options.Value;
            _tableClient = new TableClient(_azureConfig.StorageCon, _azureConfig.StorageTable);
            _tableClient.CreateIfNotExists();
        }

        public async Task<Response> CreateMessage(Confirmation message)
        {
            try
            {
                var result = await _tableClient.AddEntityAsync(message);
                return result;
            }
            catch (Exception ex)
            {

                throw ;
            }
        }

        public async Task<Response<Confirmation>> RetrieveMessage(string partitionKey, string rowKey)
        {
            var message = await _tableClient.GetEntityAsync<Confirmation>(partitionKey, rowKey);

            return message;
        }

        public List<Confirmation> RetrieveMessages(string partitionKey)
        {
            var messages = _tableClient.Query<Confirmation>(h => h.PartitionKey == partitionKey);

            List<Confirmation> resp = new();

            foreach (Confirmation qEntity in messages)
            {
                resp.Add(new Confirmation
                {
                    OrderId = qEntity.OrderId,
                    OrderStatus = qEntity.OrderStatus,
                    AgentId = qEntity.AgentId,
                    Timestamp = qEntity.Timestamp,
                    RowKey = qEntity.RowKey,
                    ETag = qEntity.ETag,
                    PartitionKey = qEntity.PartitionKey

                });
            }

            return resp;
        }

    }
}
