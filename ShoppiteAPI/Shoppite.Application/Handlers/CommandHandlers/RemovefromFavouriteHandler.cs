using MediatR;
using Shoppite.Application.Commands;
using Shoppite.Core.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Shoppite.Application.Handlers.CommandHandlers
{
    public class RemovefromFavouriteHandler: IRequestHandler<RemovefromFavourite,string>
    {
        private readonly ICartRepository _cartRepository;

        public RemovefromFavouriteHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public async Task<string> Handle(RemovefromFavourite request, CancellationToken cancellationToken)
        {
             await _cartRepository.RemovefromFavourite(request.ProductId,request.UserId,request.OrgId);
             return "Success";
        }

    }
}
