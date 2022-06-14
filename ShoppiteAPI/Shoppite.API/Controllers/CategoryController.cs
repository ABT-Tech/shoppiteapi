using Shoppite.Application.Commands;
using Shoppite.Application.Queries;
using Shoppite.Application.Responses;
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
        public async Task<object> GetCategoriesAndSubCategories()
        {
            var getCategoryDetails = await _mediator.Send(new GetAllCategoryQuery());
            var groupedCategoryDetails = getCategoryDetails.GroupBy(u =>new { u.category_id,u.category_name,u.category_image })
                                      .Select(grp => new { CategoryId = grp.Key.category_id, grp.Key.category_name, grp.Key.category_image, SubCategoryList = grp.ToList() })
                                      .ToList();
            return groupedCategoryDetails;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CategoryResponse>> CreateCategory([FromBody] CreateCategoryCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
