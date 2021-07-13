using Azure;
using MediatR;
using Mediavalet.Domain.Commands;
using Mediavalet.Domain.Interfaces;
using MediaValet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mediavalet.Domain.Handlers
{
    public class ProcessOrderHandler : IRequestHandler<ProcessOrderCommand, Response>
    {
        private readonly IAzureTableStorage _azureTableStorage;
        public ProcessOrderHandler(IAzureTableStorage azureTableStorage)
        {
            _azureTableStorage = azureTableStorage;
        }
        public async Task<Response> Handle(ProcessOrderCommand request, CancellationToken cancellationToken)
        {
            return  await _azureTableStorage.CreateMessage(request.Confirmation);
        }
    }
}
