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
    public class CreateAuthHandler : IRequestHandler<CreateAuthCommand, string>
    {
        private readonly IJwtAuth _jwtAuth;

        public CreateAuthHandler(IJwtAuth jwtAuth)
        {
            _jwtAuth = jwtAuth;
        }
        public async Task<string> Handle(CreateAuthCommand request, CancellationToken cancellationToken)
        {
            return await _jwtAuth.Authentication(request.UserCredentials.UserName, request.UserCredentials.Password); 
        }
    }
}
