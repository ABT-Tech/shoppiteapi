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
    public class UpadteUserStatusHandler: IRequestHandler<UpdateUserStatus, string>
    {
        private readonly IUserRepository _userRepository;
        public UpadteUserStatusHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<string> Handle(UpdateUserStatus request, CancellationToken cancellationToken)
        {
            return await _userRepository.UpdateUserStatus(request.cinfo);
        }
    }
}
