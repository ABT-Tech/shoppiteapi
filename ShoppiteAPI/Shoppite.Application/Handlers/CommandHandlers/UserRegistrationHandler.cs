using MediatR;
using Shoppite.Application.Commands;
using Shoppite.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shoppite.Application.Handlers.CommandHandlers
{
   public class UserRegistrationHandler:IRequestHandler<UserRegistration, string>
    {
        private readonly IUserRepository _userRepository;
        public UserRegistrationHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<string> Handle(UserRegistration request, CancellationToken cancellationToken)
        {
            await _userRepository.UserRegistration(request.RegistrationDTO);
            return "Success";
        }
    }
}
