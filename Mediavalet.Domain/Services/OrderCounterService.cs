using Azure.Data.Tables;
using Mediavalet.Domain.Interfaces;
using MediaValet.Core.Entities;
using MediaValet.Core.Utilities;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;

namespace Mediavalet.Domain.Services
{
    public class OrderCounterService : IOrderCounterService
    {
        private AzureConfig _azureConfig;
        private TableClient _tableClient;

        public OrderCounterService(IOptionsSnapshot<AzureConfig> options)
        {
            _azureConfig = options.Value;
            _tableClient = new TableClient(_azureConfig.StorageCon, _azureConfig.OrderCounter);
            _tableClient.CreateIfNotExists();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetNextOrderId()
        {
            // Side Note: The Order Supervisor is responsible for keeping the order counter up to date (initial value must be 0). It 
            // can either use an internal counter or store it externally.
            // OrderId, an incremental number 
            int orderId = 0; // OrderId, an incremental number 

            var orderCounter = _tableClient.Query<OrderCounter>(h => h.PartitionKey == _azureConfig.OrderCounter);

            if (!orderCounter.Any())
            {
                // Do a new insert with OrderId initialized to zero
                await _tableClient.AddEntityAsync(new OrderCounter(_azureConfig.OrderCounter, 1));

                return orderId;
            }
            else
            {
                var lastSavedOrder = _tableClient.Query<OrderCounter>(h => h.PartitionKey == _azureConfig.OrderCounter).OrderByDescending(h => h.Timestamp).First();

                // Increment Order Id and save new value
                lastSavedOrder.OrderId += 1;

                _tableClient.UpdateEntity(lastSavedOrder, lastSavedOrder.ETag);

                return lastSavedOrder.OrderId;
            }

        }
    }
}
