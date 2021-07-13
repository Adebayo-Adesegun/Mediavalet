using Azure.Storage.Queues.Models;
using MediatR;
using MediaValet.Core.DTOs;
using MediaValet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediavalet.Domain.Commands
{
    public record InsertOrderCommand(OrderDTO Order):IRequest<SendReceipt>;
}
