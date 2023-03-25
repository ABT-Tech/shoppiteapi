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
    public class GetMyOrderDetailHandler: IRequestHandler<GetMyOrderDetailsQuery, List<MyOrderResponse>>
    {
        private readonly IOrderRepository _orderRepository;
        public GetMyOrderDetailHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<List<MyOrderResponse>> Handle(GetMyOrderDetailsQuery request, CancellationToken cancellationToken)
        {
            var cart = await _orderRepository.GetMyOrderDetails(request.org_id, request.UserId);
            var mapper = ObjectMapper.Mapper.Map<List<MyOrderResponse>>(cart);
            return mapper;
        }
    }
}
