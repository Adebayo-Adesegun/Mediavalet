using Azure;
using Azure.Storage.Queues.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Mediavalet.Domain.Interfaces
{
    public interface IAzureQueue
    {
        Task<bool> CreateQueue();
        Task<Response> DeleteMessagesFromQueue(QueueMessage message, CancellationToken cancellationToken);
        Task DeleteQueue();
        Task<int> GetQueueLength();
        Task<SendReceipt> InsertMessageToQueueAsync(string message);
        Task<PeekedMessage[]> PeekMessage(int maxMessages);
        Task<QueueMessage[]> ReceiveMessage();
        Task<bool> UpdateMessageInQueue(SendReceipt receipt, string message);
    }
}