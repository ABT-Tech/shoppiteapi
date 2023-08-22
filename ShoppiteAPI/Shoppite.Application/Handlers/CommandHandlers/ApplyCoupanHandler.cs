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
    public class ApplyCoupanHandler : IRequestHandler<ApplyCoupan, string>
    {
        private readonly IUserRepository _userRepository;

        public ApplyCoupanHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<string> Handle(ApplyCoupan request, CancellationToken cancellationToken)
        {
            return await _userRepository.ApplyCoupan(request.coupan);
             
        }
    }
}
