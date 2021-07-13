using Azure.Storage.Queues.Models;
using MediatR;
using Mediavalet.Domain.Interfaces;
using Mediavalet.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mediavalet.Domain.Handlers
{
    public class GetOrdersHandler : IRequestHandler<GetOrdersQuery, QueueMessage[]>
    {
        private readonly IAzureQueue _azureQueue;
        public GetOrdersHandler(IAzureQueue azureQueue)
        {
            _azureQueue = azureQueue;
        }

        public async Task<QueueMessage[]> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _azureQueue.ReceiveMessage();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
