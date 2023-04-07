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
    public class GetOrderDetailsByOrgIdHandler: IRequestHandler<GetOrdersDetailByOrgId, List<OrderDetailResponse>>
    {
        private readonly IOrderRepository _orderRepository;
        public GetOrderDetailsByOrgIdHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<List<OrderDetailResponse>> Handle(GetOrdersDetailByOrgId request, CancellationToken cancellationToken)
        {
            var cart = await _orderRepository.GetOrderDetailsByOrgId(request.OrgId);
            var mapper = ObjectMapper.Mapper.Map<List<OrderDetailResponse>>(cart);
            return mapper;
        }
    }
}
