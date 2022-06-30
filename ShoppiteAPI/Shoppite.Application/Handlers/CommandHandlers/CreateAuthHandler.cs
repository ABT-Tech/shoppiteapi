using MediatR;
using Shoppite.Application.Commands;
using Shoppite.Core.DTOs;
using Shoppite.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shoppite.Application.Handlers.CommandHandlers
{
    public class CreateAuthHandler : IRequestHandler<CreateAuthCommand, Users_DTO>
    {
        private readonly IJwtAuth _jwtAuth;

        public CreateAuthHandler(IJwtAuth jwtAuth)
        {
            _jwtAuth = jwtAuth;
        }
        public async Task<Users_DTO> Handle(CreateAuthCommand request, CancellationToken cancellationToken)
        {
            return await _jwtAuth.Authentication(request.UserCredentials.email, request.UserCredentials.password, request.UserCredentials.org_id); 
        }
    }
}
