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
    public class UpdateOrderStatusHandler : IRequestHandler<UpdateOrderStatus, string>
    {
        private readonly IOrderRepository _orderRepository;
        public UpdateOrderStatusHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<string> Handle(UpdateOrderStatus request, CancellationToken cancellationToken)
        {
            await _orderRepository.UpdateOrderStatus(request.orders);
            return "Success";
        }
    }
}
