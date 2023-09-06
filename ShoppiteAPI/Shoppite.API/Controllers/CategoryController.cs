using Shoppite.Application.Commands;
using Shoppite.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoppite.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<object> GetAllCategories(int? OrgId,int OrgCategoryId)
        {
            return await _mediator.Send(new GetAllCategoriesQuery(OrgId, OrgCategoryId));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<object> GetAllCarosolCategories(int OrgId)
        {
            return await _mediator.Send(new GetAllCarosolCategories(OrgId));
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<object> GetAllParentCategories()
        {
            return await _mediator.Send(new GetAllParentCategories());
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<object> GetAllCategoriesByParent(int MainCategoryId)
        {
            return await _mediator.Send(new GetAllCategoriesByParent(MainCategoryId));
        }
    }
}
