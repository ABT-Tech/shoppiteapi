using Shoppite.Application.Commands;
using Shoppite.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shoppite.Core.DTOs;

namespace Shoppite.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<object> GetUserById(int org_id, int user_id)
        {
            return await _mediator.Send(new GetUserByIdQuery(org_id, user_id));
        }
    }
}
