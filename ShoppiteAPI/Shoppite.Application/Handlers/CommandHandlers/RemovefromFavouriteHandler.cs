using MediatR;
using Shoppite.Application.Commands;
using Shoppite.Core.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Shoppite.Application.Handlers.CommandHandlers
{
    public class RemovefromFavouriteHandler: IRequestHandler<RemovefromFavourite,int>
    {
        private readonly ICartRepository _cartRepository;

        public RemovefromFavouriteHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public async Task<int> Handle(RemovefromFavourite request, CancellationToken cancellationToken)
        {
             await _cartRepository.RemovefromFavourite(request.ProductId,request.UserId,request.OrgId);
             return 0;
        }

    }
}
