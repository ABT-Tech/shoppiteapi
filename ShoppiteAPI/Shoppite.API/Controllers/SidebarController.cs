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
    public class SidebarController : Controller
    {
        private readonly IMediator _mediator;
        public SidebarController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<Core.DTOs.Sidebar_DTO>> GetSidebar()
        {
            return await _mediator.Send(new GetAllSidebarQuery());
        }
    }
}
