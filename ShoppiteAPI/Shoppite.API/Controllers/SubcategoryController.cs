using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Shoppite.Application.Commands;
using Shoppite.Application.Queries;
using Shoppite.Core.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace Shoppite.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SubcategoryController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _iconfiguration;
        public SubcategoryController(IMediator mediator, IConfiguration iconfiguration)
        {
            _mediator = mediator;
            _iconfiguration = iconfiguration;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<Core.DTOs.Subcatgory_DTO> AddSubcategory([FromForm]Subcatgory_DTO subcatgory_DTO)
        {
            string SystemPath = _iconfiguration.GetSection("SystemPath").Value+"\\"+subcatgory_DTO.org_id;
            if (!Directory.Exists(SystemPath)) {
                Directory.CreateDirectory(SystemPath);
            }
            SystemPath = SystemPath + "\\SubCategory";
            if (!Directory.Exists(SystemPath))
            {
                Directory.CreateDirectory(SystemPath);
            }
            SystemPath = SystemPath + "\\"+subcatgory_DTO.id;
            if (!Directory.Exists(SystemPath))
            {
                Directory.CreateDirectory(SystemPath);
            }
            string path = Path.Combine(SystemPath, subcatgory_DTO.subcategory_file.FileName);
           // string path = Path.Combine(Directory.GetDirectories(@"C:\Images"));
            using(Stream stream = new FileStream(path,FileMode.Create))
            {
                subcatgory_DTO.subcategory_file.CopyTo(stream);
            }
            subcatgory_DTO.sub_ctg_image = path;
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
        [Route("{org_id}/{sub_category_id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<Core.DTOs.Subcatgory_DTO>> GetSubcategoryBySubCategoryId(int org_id, int sub_category_id)
        {
            return await _mediator.Send(new GetSubcategoryByIdQuery(sub_category_id,org_id));
        }

        [HttpGet]
        [Route("{id}/{org_id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<Core.DTOs.Subcatgory_DTO>> DeleteSubCategory(int id, int org_id)
        {
            return await _mediator.Send(new DeleteSubCategoryQuery(id, org_id));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<Core.DTOs.Subcatgory_DTO> UpdateSubCategory(Subcatgory_DTO subcatgory_DTO)
        {
            return await _mediator.Send(new UpdateSubcategoryCommand(subcatgory_DTO));
        }

    }
}
