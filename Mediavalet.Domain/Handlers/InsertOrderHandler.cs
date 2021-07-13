using Azure.Storage.Queues.Models;
using MediatR;
using Mediavalet.Domain.Commands;
using Mediavalet.Domain.Interfaces;
using MediaValet.Core.DTOs;
using MediaValet.Core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mediavalet.Domain.Handlers
{
    public class InsertOrderHandler : IRequestHandler<InsertOrderCommand, SendReceipt>
    {
        private readonly IAzureQueue _azureQueue;
        private readonly IOrderCounterService _orderCounter;
        public InsertOrderHandler(IAzureQueue azureQueue, IOrderCounterService orderCounter)
        {
            _azureQueue = azureQueue;
            _orderCounter = orderCounter;
        }
        public async Task<SendReceipt> Handle(InsertOrderCommand request, CancellationToken cancellationToken)
        {
            if (request.Order == null || string.IsNullOrEmpty(request.Order.OrderText))
                throw new ArgumentException("please provide a valid order");

            Order order = new(request.Order.OrderText, await _orderCounter.GetNextOrderId());

            string message = JsonConvert.SerializeObject(order);

            return await _azureQueue.InsertMessageToQueueAsync(message);
        }
    }
}
