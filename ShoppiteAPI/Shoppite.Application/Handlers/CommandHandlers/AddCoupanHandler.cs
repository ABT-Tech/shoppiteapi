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
    class AddCoupanHandler : IRequestHandler<AddCoupan, string>
    {
        private readonly IUserRepository _userRepository;
        public AddCoupanHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<string> Handle(AddCoupan request, CancellationToken cancellationToken)
        {
            await _userRepository.AddCoupan(request.coupan);
            return "Success";
        }
    }
}
