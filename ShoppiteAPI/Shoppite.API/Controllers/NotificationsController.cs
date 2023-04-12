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
using Shoppite.Infrastructure.Helpers;

namespace Shoppite.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public NotificationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<string> SetUserFirebaseToken(FireBaseToken fireBaseToken)
        {
            return await _mediator.Send(new AddFirebaseToken(fireBaseToken));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> UserRegistration([FromBody] CreateAuthCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public string UserGetPassword(string Password)
        //{
        //    EncryptionHelper encryptionHelper = new EncryptionHelper();
        //    return encryptionHelper.Decrypt(Password);
        //}
    }
}
