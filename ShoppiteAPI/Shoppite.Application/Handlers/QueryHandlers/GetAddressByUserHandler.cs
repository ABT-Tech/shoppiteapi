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
    public class GetAddressByUserHandler : IRequestHandler<GetAddressByUserid, List<AddressResponse>>
    {
        private readonly ICartRepository _cartRepository;
        public GetAddressByUserHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public async Task<List<AddressResponse>> Handle(GetAddressByUserid request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetAddressByUserId(request.org_id, request.UserId);
            var mapper = ObjectMapper.Mapper.Map<List<AddressResponse>>(cart);
            return mapper;
        }
    }
}
