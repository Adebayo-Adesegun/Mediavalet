using Azure;
using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaValet.Core.Entities
{
    public class OrderCounter : ITableEntity
    {
        public OrderCounter(string tableName, int tabledId)
        {
            PartitionKey = tableName;
            RowKey = Convert.ToString(tabledId);
        }

        public OrderCounter() { }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
        public int OrderId { get; set; }
    }
}
