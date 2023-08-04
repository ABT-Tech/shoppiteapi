using MediatR;
using Shoppite.Application.Commands;
using Shoppite.Core.DTOs;
using Shoppite.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shoppite.Application.Handlers.CommandHandlers
{
    public class MakePaymentRequestHandler : IRequestHandler<PaymentRequest, PaymentGatewayResponse>
    {
        private readonly IOrderRepository _orderRepository;
        public MakePaymentRequestHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<PaymentGatewayResponse> Handle(PaymentRequest request, CancellationToken cancellationToken)
        {
            return await _orderRepository.MakePaymentRequest(request.orders);
        }
    }
}
