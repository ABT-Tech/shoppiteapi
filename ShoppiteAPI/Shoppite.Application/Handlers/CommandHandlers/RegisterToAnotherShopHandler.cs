using MediatR;
using Shoppite.Application.Commands;
using Shoppite.Core.DTOs;
using Shoppite.Core.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Shoppite.Application.Handlers.CommandHandlers
{
    public class RegisreToAnotherShopHandler : IRequestHandler<RegisterToanotherOrg, string>
    {
        private readonly IUserRepository _userRepository;

        public RegisreToAnotherShopHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<string> Handle(RegisterToanotherOrg request, CancellationToken cancellationToken)
        {
            return await _userRepository.RegisterToanotherOrg(request.ExistanceDTO);
        }
    }
}
