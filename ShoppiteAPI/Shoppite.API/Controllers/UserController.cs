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
     //   private EncryptionHelper eh = new EncryptionHelper();
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
        [HttpPost]
        public async Task<ActionResult<string>> UserRegistration([FromBody] UserRegistration user)
        {
            string Password = user.RegistrationDTO.Password;
          //  string encryptedpassword = eh.Encrypt(Password);
            //user.RegistrationDTO.Password = encryptedpassword;
            return await _mediator.Send(user);
        }
    }
}
