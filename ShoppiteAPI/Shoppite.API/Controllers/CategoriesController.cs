using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoppite.Application.Commands;
using Shoppite.Application.Queries;
using Shoppite.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoppite.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly IMediator _mediator;
        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<Core.DTOs.Category_DTO> AddCategory(Category_DTO category_DTO)
        {
            return await _mediator.Send(new CreateCategoriesCommand(category_DTO));
        }

        [HttpGet]
        [Route("{org_id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<Core.DTOs.Category_DTO>> GetCategory(int org_id)
        {
            return await _mediator.Send(new GetAllCategoriesQuery(org_id));
        }

        [HttpGet]
        [Route("{id}/{org_id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<Core.DTOs.Category_DTO>> DeleteCategory(int id, int org_id)
        {
            return await _mediator.Send(new DeleteCategoryQuery(id,org_id));
        }

        [HttpGet]
        [Route("{id}/{org_id}/{category_code}/{category_name}/{category_description}/{category_image}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<Core.DTOs.Category_DTO>> UpdateCategory(int id, int org_id, string category_code, string category_name, string category_description, string category_image)
        {
            return await _mediator.Send(new UpdateCategorytQuery(id, org_id, category_code, category_name, category_description, category_image));
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<Core.DTOs.Category_DTO>> GetCategorybyid(int id)
        {
            return await _mediator.Send(new GetCategorybyidQuery(id));
        }
    }
}
