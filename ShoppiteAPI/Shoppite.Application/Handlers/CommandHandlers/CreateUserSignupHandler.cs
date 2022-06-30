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
    public class CreateUserSignupHandler : IRequestHandler<CreateUserSignupCommand, Users_DTO>
    {
        private readonly IUserRepository _createUserRepository;

        public CreateUserSignupHandler(IUserRepository createUserRepository)
        {
            _createUserRepository = createUserRepository;
        }
        public async Task<Users_DTO> Handle(CreateUserSignupCommand request, CancellationToken cancellationToken)
        {
            return await _createUserRepository.PostUserSignup(request.user);
        }
    }
}
