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
    public class CreateAddOrderHandler : IRequestHandler<CreateOrderCommand, Order_DTO>
    {
        private readonly IOrderRepository _createOrderRepository;

        public CreateAddOrderHandler(IOrderRepository createOrderRepository)
        {
            _createOrderRepository = createOrderRepository;
        }
        public async Task<Order_DTO> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            return await _createOrderRepository.PostOrder(request.order);
        }
    }
}
