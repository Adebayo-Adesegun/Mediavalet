using MediatR;
using Mediavalet.Domain.Interfaces;
using Mediavalet.Domain.Queries;
using MediaValet.Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mediavalet.Domain.Handlers
{
    public class GetOrdersConfirmationHandler : IRequestHandler<GetOrderConfirmationsQuery, List<Confirmation>>
    {
        private readonly IAzureTableStorage _azureTableStorage;
        public GetOrdersConfirmationHandler(IAzureTableStorage azureTableStorage)
        {
            _azureTableStorage = azureTableStorage;
        }

        public async Task<List<Confirmation>> Handle(GetOrderConfirmationsQuery request, CancellationToken cancellationToken)
        {
            return  _azureTableStorage.RetrieveMessages("ConfirmationKey");
        }
    }
}
