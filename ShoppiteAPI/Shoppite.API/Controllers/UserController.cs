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
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private EncryptionHelper eh = new EncryptionHelper();
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
        public async Task<ActionResult<string>> UserRegistration([FromBody] UserRegistrationDTO user)
        {
            string Password = user.Password;
            string encryptedpassword = eh.Encrypt(Password);
            user.Password = encryptedpassword;
            return await _mediator.Send(new UserRegistration(user));
        }
        [HttpPost]
        public async Task<ActionResult<string>> UpdateUserProfile([FromBody] UserDTO user)
        {
            return await _mediator.Send(new EditUserProfile(user));
        }
        [HttpPost]
        public async Task<string> ForgetPassword([FromBody] ForgetPassword password)
        {
            string enpassword = eh.Encrypt(password.Password);
            string enConfirmpassword = eh.Encrypt(password.ConfirmPassword);
            password.Password = enpassword;
            password.ConfirmPassword = enConfirmpassword;
            return await _mediator.Send(new Forgetpassword(password));
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<object> GetCustomerDetails(int OrgId)
        {
            return await _mediator.Send(new GetCustomerDetails(OrgId));
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<object> GetTotalorderDetails(int OrgId)
        {
            return await _mediator.Send(new GetTotalOrderDetails(OrgId));
        }
        [HttpPost]
        public async Task<object> UpdateUserStatus(CustomerInfo info)
        {
            return await _mediator.Send(new UpdateUserStatus(info));
        }
        [HttpPost]
        public async Task<ActionResult<string>> AddCoupan([FromBody] CoupanDTO coupan)
        {
            return await _mediator.Send(new AddCoupan(coupan));
        }
        [HttpPost]
        public async Task<ActionResult<UserCoupanResponse>> ApplyCoupon([FromBody] CoupanDTO coupan)
        {
            return await _mediator.Send(new ApplyCoupan(coupan));
        }
    }
}
