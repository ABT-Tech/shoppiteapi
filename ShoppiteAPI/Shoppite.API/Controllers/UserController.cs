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
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<Core.DTOs.Users_DTO> PostUserSignup(Users_DTO users_DTO)
        {
            return await _mediator.Send(new CreateUserSignupCommand(users_DTO));
        }

        [HttpGet]
        [Route("{org_id}/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<Core.DTOs.UserInfo_DTO>> GetUserInfo(int org_id, int id)
        {
            return await _mediator.Send(new GetAllUserQuery(org_id, id));
        }
    }
}
