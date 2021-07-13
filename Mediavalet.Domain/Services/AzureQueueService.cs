using Azure;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Mediavalet.Domain.Interfaces;
using MediaValet.Core.Utilities;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mediavalet.Domain.Services
{
    public class AzureQueueService : IAzureQueue
    {
        private AzureConfig _azureConfig;
        private QueueClient _queueClient;
        public AzureQueueService(IOptionsSnapshot<AzureConfig> options)
        {
            _azureConfig = options.Value;
            _queueClient = new QueueClient(_azureConfig.StorageCon, _azureConfig.QueueName);
            _queueClient.CreateIfNotExists();

        }
        public async Task<SendReceipt> InsertMessageToQueueAsync(string message)
        {
            try
            {
                // Save the receipt so we can update this message later
                SendReceipt receipt = await _queueClient.SendMessageAsync(message);

                return receipt;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<QueueMessage[]> ReceiveMessage()
        {
            try
            {
                // Get messages from the queue
                QueueMessage[] messages = await _queueClient.ReceiveMessagesAsync();

                return messages;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}\n\n");
                throw;
            }
        }

        public async Task<bool> CreateQueue()
        {
            try
            {
                await _queueClient.CreateIfNotExistsAsync();

                if (_queueClient.Exists())
                {
                    return true;
                }

                return false;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<PeekedMessage[]> PeekMessage(int maxMessages)
        {
            try
            {
                PeekedMessage[] peekedMessages = await _queueClient.PeekMessagesAsync(maxMessages: maxMessages);

                return peekedMessages;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> UpdateMessageInQueue(SendReceipt receipt, string message)
        {
            try
            {
                var response = await _queueClient.UpdateMessageAsync(receipt.MessageId, receipt.PopReceipt, message);

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Response> DeleteMessagesFromQueue(QueueMessage message, CancellationToken cancellationToken)
        {
            // Let the service know we're finished with
            // the message and it can be safely deleted.
            return await _queueClient.DeleteMessageAsync(message.MessageId, message.PopReceipt, cancellationToken);
        }


        public async Task<int> GetQueueLength()
        {
            if (_queueClient.Exists())
            {
                QueueProperties properties = await _queueClient.GetPropertiesAsync();

                // Retrieve the cached approximate message count.
                int cachedMessagesCount = properties.ApproximateMessagesCount;

                return cachedMessagesCount;
            }
            return 0;
        }

        public async Task DeleteQueue()
        {
            await _queueClient.DeleteAsync();
        }
    }
}
