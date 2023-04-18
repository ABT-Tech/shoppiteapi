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
        public async Task<object> GetAllProductsByOrganizations(int org_Id,int? userId)
        {
            return await _mediator.Send(new GetAllProductsByOrganizationsQuery(org_Id,userId));
        }

       /* [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<object> GetMostSellerProductsByOrganizations(int org_Id)
        {
            return await _mediator.Send(new GetAllProductsByOrganizationsQuery(org_Id));
        }*/

       /* [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<object> GetLastVisitedProductsByOrganizations(int org_Id)
        {
            return await _mediator.Send(new GetAllProductsByOrganizationsQuery(org_Id));
        }*/
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
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<object> SearchProducts(int org_Id,string productname)
        {
            return await _mediator.Send(new SearchProduct(org_Id,productname));
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<object> GetRecentlyViewedProductsByCategory(int org_Id, int CategoryId,string IP)
        {
            return await _mediator.Send(new GetRecentlyViewedProductsByCategory(org_Id, CategoryId,IP));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<object> GetMostViewedProductsByCategory(int org_Id, int CategoryId, string IP)
        {
            return await _mediator.Send(new GetMostViewedProductsByCategory(org_Id, CategoryId, IP));
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<object> GetProductsByBestSellers(int org_Id)
        {
            return await _mediator.Send(new GetProductsByBestSellers(org_Id));
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<object> GetAllProductsByCategory(int OrgId, int CategoryId)
        {
            return await _mediator.Send(new GetProductsByCategory(OrgId, CategoryId));
        }
    }
}
