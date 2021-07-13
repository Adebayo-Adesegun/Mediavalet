using Azure;
using MediatR;
using MediaValet.Core.Entities;
using System.Collections.Generic;
using System.Threading;

namespace Mediavalet.Domain.Queries
{
    public record GetOrderConfirmationsQuery(CancellationToken CancellationToken): IRequest<List<Confirmation>>;
}
