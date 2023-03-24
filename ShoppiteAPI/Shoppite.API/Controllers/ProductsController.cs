using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoppite.Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoppite.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<object> GetAllProductsByOrganizations(int org_Id)
        {
            return await _mediator.Send(new GetAllProductsByOrganizationsQuery(org_Id));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<object> GetMostSellerProductsByOrganizations(int org_Id)
        {
            return await _mediator.Send(new GetAllProductsByOrganizationsQuery(org_Id));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<object> GetLastVisitedProductsByOrganizations(int org_Id)
        {
            return await _mediator.Send(new GetAllProductsByOrganizationsQuery(org_Id));
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<object> GetWishlistByUser(int org_Id, int user_Id)
        {
            return await _mediator.Send(new GetWishlistByUserQuery(org_Id, user_Id));
        }

      /*  [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<object> GetMostSellerProductsByOrganizations(int org_Id)
        {
            return await _mediator.Send(new GetAllProductsByOrganizationsQuery(org_Id));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<object> GetLastVisitedProductsByOrganizations(int org_Id)
        {
            return await _mediator.Send(new GetAllProductsByOrganizationsQuery(org_Id));
        } */
    }
}
