using MediatR;
using Shoppite.Application.Commands;
using Shoppite.Core.DTOs;
using Shoppite.Core.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Shoppite.Application.Handlers.CommandHandlers
{
    public class VerifyUserExistenceHandler : IRequestHandler<VerifyUser, UserRegisteredCheckResponse>
    {
        private readonly IUserRepository _userRepository;

        public VerifyUserExistenceHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserRegisteredCheckResponse> Handle(VerifyUser request, CancellationToken cancellationToken)
        {
            return await _userRepository.UserVerify(request.ExistanceDTO);

        }
    }
}
