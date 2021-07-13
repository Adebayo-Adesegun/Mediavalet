using Azure;
using Azure.Storage.Queues.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediavalet.Domain.Commands
{
    public record DeleteOrderFromQueueCommand(QueueMessage Message):IRequest<Response>;
}
