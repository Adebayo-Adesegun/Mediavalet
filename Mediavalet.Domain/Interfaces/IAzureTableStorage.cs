using Azure;
using MediaValet.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mediavalet.Domain.Interfaces
{
    public interface IAzureTableStorage
    {
        Task<Response> CreateMessage(Confirmation message);
        Task<Response<Confirmation>> RetrieveMessage(string partitionKey, string rowKey);
        List<Confirmation> RetrieveMessages(string partitionKey);
    }
}
