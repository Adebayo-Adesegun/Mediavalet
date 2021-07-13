using Azure;
using MediatR;
using Mediavalet.Domain.Commands;
using Mediavalet.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mediavalet.Domain.Handlers
{
    public class DeleteOrderFromQueueHandler : IRequestHandler<DeleteOrderFromQueueCommand, Response>

    {
        private readonly IAzureQueue _azureQueue;
        public DeleteOrderFromQueueHandler(IAzureQueue azureQueue)
        {
            _azureQueue = azureQueue;
        }

        public async Task<Response> Handle(DeleteOrderFromQueueCommand request, CancellationToken cancellationToken)
        {
            return await _azureQueue.DeleteMessagesFromQueue(request.Message, cancellationToken);
        }
    }
}
