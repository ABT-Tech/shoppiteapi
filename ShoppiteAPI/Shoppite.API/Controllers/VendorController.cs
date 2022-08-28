using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoppite.Application.Commands;
using Shoppite.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoppite.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VendorController : Controller
    {
        private readonly IMediator _mediator;
        public VendorController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<Core.DTOs.Vendor_DTO> AddVendor(Vendor_DTO vendor)
        {
            return await _mediator.Send(new CreateVendorCommand(vendor));
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<Core.DTOs.Vendor_Users_DTO> AddVendorUsers(Vendor_Users_DTO vendorusers)
        {
            return await _mediator.Send(new CreateVendorUsersCommand(vendorusers));
        }
        //[HttpGet]
        //[Route("{org_id}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public async Task<List<Core.DTOs.Vendor_Users_DTO>> GetAllVendorUsers(int org_id)
        //{
        //    return await _mediator.Send(new GetAllVendorUsersQuery(org_id));
        //}
    }
}
