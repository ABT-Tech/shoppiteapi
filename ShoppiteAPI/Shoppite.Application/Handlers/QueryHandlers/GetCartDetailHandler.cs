using AutoMapper;
using MediatR;
using Shoppite.Application.Mapper;
using Shoppite.Application.Queries;
using Shoppite.Application.Responses;
using Shoppite.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shoppite.Application.Handlers.QueryHandlers
{
    public class GetCartDetailHandler: IRequestHandler<GetCartDetailsQuery, List<CartResponse>>
    {
        private readonly ICartRepository _cartRepository;
        public GetCartDetailHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public async Task<List<CartResponse>> Handle(GetCartDetailsQuery request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetCartDetails(request.org_id,request.UserId);
            var mapper = ObjectMapper.Mapper.Map<List<CartResponse>>(cart);
            return mapper;
        }
    }
}
