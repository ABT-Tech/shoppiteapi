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
   public class GetOrderDetailsForVendorHandler:IRequestHandler<GetOrderDetails, List<VendorsOrderResponse>>
    {
        private readonly IOrderRepository _orderRepository;
        public GetOrderDetailsForVendorHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<List<VendorsOrderResponse>> Handle(GetOrderDetails request, CancellationToken cancellationToken)
        {
            var cart = await _orderRepository.GetOrdersDetailForVendor(request.OrgId);
            var mapper = ObjectMapper.Mapper.Map<List<VendorsOrderResponse>>(cart);
            return mapper;
        }
    }
}
