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
    public class SubcategoryController : Controller
    {
        private readonly IMediator _mediator;
        public SubcategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<Core.DTOs.Subcatgory_DTO> AddSubcategory(Subcatgory_DTO subcatgory_DTO)
        {
            return await _mediator.Send(new CreateSubcategoryCommand(subcatgory_DTO));
        }

        [HttpGet]
        [Route("{org_id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<Core.DTOs.Subcatgory_DTO>> GetAllSubcategory(int org_id)
        {
            return await _mediator.Send(new GetAllSubcategoryQuery(org_id));
        }

        [HttpGet]
        [Route("{org_id}/{category_id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<Core.DTOs.Subcatgory_DTO>> GetAllSubcategoryByCategory(int org_id, int category_id)
        {
            return await _mediator.Send(new GetAllSubcategoryByCategoryQuery(org_id, category_id));
        }

        [HttpGet]
        [Route("{id}/{org_id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<Core.DTOs.Subcatgory_DTO>> DeleteSubCategory(int id, int org_id)
        {
            return await _mediator.Send(new DeleteSubCategoryQuery(id, org_id));
        }

        [HttpGet]
        [Route("{id}/{org_id}/{category_id}/{sub_ctg_name}/{sub_ctg_description}/{sub_ctg_code}/{sub_ctg_image}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<Core.DTOs.Subcatgory_DTO>> UpdateSubCategory(int id, int org_id, int category_id, string sub_ctg_name, string sub_ctg_description, string sub_ctg_code, string sub_ctg_image)
        {
            return await _mediator.Send(new UpdateSubCategoryQuery(id, org_id, category_id, sub_ctg_name, sub_ctg_description, sub_ctg_code, sub_ctg_image));
        }

    }
}
