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
    public class GetTotalOrderDetailHandler:IRequestHandler<GetTotalOrderDetails, List<ReportResponse>>
    {
        private readonly IOrderRepository _orderRepository;
        public GetTotalOrderDetailHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<List<ReportResponse>> Handle(GetTotalOrderDetails request, CancellationToken cancellationToken)
        {
            var customerDetails = await _orderRepository.GetTotalOrderDetails(request.OrgId);
            var mapper = ObjectMapper.Mapper.Map<List<ReportResponse>>(customerDetails);
            return mapper;
        }
    }
}
