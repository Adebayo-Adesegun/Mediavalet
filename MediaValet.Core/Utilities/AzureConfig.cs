using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaValet.Core.Utilities
{
    public class AzureConfig
    {
        public string StorageCon { get; set; }
        public string QueueName { get; set; }
        public string StorageTable { get; set; }
        public string OrderCounter { get; set; }
    }
}
