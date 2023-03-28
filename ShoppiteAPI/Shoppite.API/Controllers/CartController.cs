using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoppite.Application.Commands;
using Shoppite.Application.Queries;
using Shoppite.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using Shoppite.Core.Model;
using System.Threading.Tasks;

namespace Shoppite.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<object> AddToCart([FromBody] CartRequest cart)
        {
            return await _mediator.Send(new AddToCartCommand(cart));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<object> GetAllCartDetails(int OrgId, int UserId)
        {
            return await _mediator.Send(new GetCartDetailsQuery(OrgId, UserId));
        }

        [HttpPost]
        public async Task<ActionResult<string>> PlaceOrder([FromBody]OrdersDTO orders)
        {
            return await _mediator.Send(new CreateOrder(orders));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<object> GetMyOrderDetails(int OrgId, int UserId)
        {
            return await _mediator.Send(new GetMyOrderDetailsQuery(OrgId, UserId));
        }
        [HttpPost]
        public async Task<string> AddtoFavourite([FromBody] Favourite favourtite)
        {
            return await _mediator.Send(new AddToFavourtite(favourtite));
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<object> GetAddressByUserId(int OrgId, int UserId)
        {
            return await _mediator.Send(new GetAddressByUserid(OrgId, UserId));
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<int> RemovefromFavourite(int ProductId,int Userid,int OrgId)
        {
            return await _mediator.Send(new RemovefromFavourite(ProductId,Userid, OrgId));
        }
    }
}
