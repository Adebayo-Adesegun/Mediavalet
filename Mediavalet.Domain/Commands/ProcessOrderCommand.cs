using Azure;
using MediatR;
using MediaValet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediavalet.Domain.Commands
{
    public record ProcessOrderCommand(Confirmation Confirmation):IRequest<Response>;
}
