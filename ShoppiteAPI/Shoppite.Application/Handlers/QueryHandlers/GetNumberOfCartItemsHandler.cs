using MediatR;
using Shoppite.Application.Mapper;
using Shoppite.Application.Queries;
using Shoppite.Application.Responses;
using Shoppite.Core.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Shoppite.Application.Handlers.QueryHandlers
{
    public class GetNumberOfCartItemsHandler: IRequestHandler<GetNumOfItemsInCart, NumberOfCartItemResponse>
    {
        private readonly ICartRepository _cartRepository;
        public GetNumberOfCartItemsHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public async Task<NumberOfCartItemResponse> Handle(GetNumOfItemsInCart request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetNoOfItemsInCart(request.OrgId,request.UserId);
            var mapper = ObjectMapper.Mapper.Map<NumberOfCartItemResponse>(cart);
            return mapper;
        }
    }
}
