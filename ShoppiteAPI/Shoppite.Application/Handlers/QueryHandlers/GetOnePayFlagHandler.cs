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
    public class GetOnePayFlagHandler : IRequestHandler<GetOnePayFlag, OrderResponse>
    {
        private readonly IOrderRepository _orderRepository;
        public GetOnePayFlagHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<OrderResponse> Handle(GetOnePayFlag request, CancellationToken cancellationToken)
        {
            var orgDetails = await _orderRepository.GetOnePayFlag(request.OrgId);
            var mapper = ObjectMapper.Mapper.Map<OrderResponse>(orgDetails);
            return mapper;
        }

    }
}
