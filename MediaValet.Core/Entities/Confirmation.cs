using Azure;
using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaValet.Core.Entities
{
    public class Confirmation : ITableEntity
    {
        public int OrderId { get; set; }
        public Guid AgentId { get; set; }
        public string OrderStatus { get; set; }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        public Confirmation(int orderId, Guid agentId, string orderStatus)
        {
            OrderId = orderId;
            AgentId = agentId;
            OrderStatus = orderStatus;
            RowKey = orderId.ToString();
            PartitionKey = "ConfirmationKey";
        }

        public Confirmation(){
}
    }
}
