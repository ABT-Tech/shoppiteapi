using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoppite.Application.Commands;
using Shoppite.Application.Queries;
using System.Threading.Tasks;

namespace Shoppite.API.Controllers
{
    public class OrderController : Controller
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /*[HttpPost]
        public async Task<ActionResult<string>> PlaceOrder([FromBody] CreateOrder orders)
        {
            return await _mediator.Send(orders);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<object> GetMyOrderDetails(int OrgId, int UserId)
        {
            return await _mediator.Send(new GetMyOrderDetailsQuery(OrgId, UserId));
        }*/
    }
}
