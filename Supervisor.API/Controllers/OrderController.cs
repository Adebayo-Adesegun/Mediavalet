using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediaValet.Core.DTOs;
using MediatR;
using Mediavalet.Domain.Queries;
using Mediavalet.Domain.Commands;

namespace Supervisor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private IMediator _mediator;
   
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderDTO orderDto)
        {

            var receipt = await _mediator.Send(new InsertOrderCommand(orderDto));

            if (receipt != null)
            {
                return Ok(new { message = "Order was placed successfully", data = receipt});
            }
            else
            {
                return BadRequest(new { message = "Order failed", data = receipt });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var confirmations = await _mediator.Send(new GetOrderConfirmationsQuery(default));

            if (confirmations == null)
                return NotFound();
             
            return Ok(confirmations);
        }
    }
}
