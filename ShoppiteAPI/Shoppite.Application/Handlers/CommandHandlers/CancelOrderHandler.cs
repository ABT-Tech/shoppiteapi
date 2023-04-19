using MediatR;
using Shoppite.Application.Commands;
using Shoppite.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shoppite.Application.Handlers.CommandHandlers
{
    public class CancelOrderHandler: IRequestHandler<CancelOrder, string>
    {
        private readonly IOrderRepository _orderRepository;
        public CancelOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<string> Handle(CancelOrder request, CancellationToken cancellationToken)
        {
            return await _orderRepository.cancelOrder(request.cancelOrder);
        }
    }
}
