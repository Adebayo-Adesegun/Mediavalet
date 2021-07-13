using Azure.Storage.Queues.Models;
using MediatR;

namespace Mediavalet.Domain.Queries
{
    public record GetOrdersQuery() : IRequest<QueueMessage[]>;
}
