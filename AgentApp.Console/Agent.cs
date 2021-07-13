using MediatR;
using Mediavalet.Domain.Commands;
using Mediavalet.Domain.Queries;
using MediaValet.Core.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AgentApp
{
    public class Agent
    {
        private readonly IMediator _mediator;

        public Agent(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Run()
        {

            var agentId = Guid.NewGuid();
            var magicNumber = new Random().Next(1, 10);

            Console.WriteLine($"I am agent {agentId}, my magic number is {magicNumber}");

            while (true)
            {
                var queue = await _mediator.Send(new GetOrdersQuery());

                foreach(var message in queue)
                {
                    var deserializedOrder = JsonConvert.DeserializeObject<Order>(message.Body.ToString());

                    Console.WriteLine($"Received order #{deserializedOrder.OrderId}");



                    if (magicNumber == deserializedOrder.RandomNumber)
                    {
                        Console.WriteLine($"Oh no, my magic number was found");

                        break;

                    }
                    else
                    {
                        Console.WriteLine($"Order Text : {deserializedOrder.OrderText}");

                        var result = await _mediator.Send(new ProcessOrderCommand(new Confirmation(deserializedOrder.OrderId, agentId, "Processed")));

                        if (result.Status == 204)
                        {
                            // Delete Message from queue
                            await _mediator.Send(new DeleteOrderFromQueueCommand(message));
                        }
                    }
                }
            }
        }

    }
}
