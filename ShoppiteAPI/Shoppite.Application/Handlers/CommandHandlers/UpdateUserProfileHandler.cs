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
    public class UpdateUserProfileHandler:IRequestHandler<EditUserProfile, string>
    {
        private readonly IUserRepository _userRepository;
        public UpdateUserProfileHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<string> Handle(EditUserProfile request, CancellationToken cancellationToken)
        {
            await _userRepository.UpdateUserProfile(request.UserDTO);
            return "Success";
        }
    }
}
