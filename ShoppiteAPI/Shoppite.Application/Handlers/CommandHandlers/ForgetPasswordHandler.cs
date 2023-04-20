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
    public class ForgetPasswordHandler : IRequestHandler<Forgetpassword, string>
    {
        private readonly IUserRepository _userRepository;

        public ForgetPasswordHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<string> Handle(Forgetpassword request, CancellationToken cancellationToken)
        {
           return await _userRepository.ForgetPassword(request.Password);
            
        }
    }
}
