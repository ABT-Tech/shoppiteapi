using AutoMapper;
using MediatR;
using Shoppite.Application.Commands;
using Shoppite.Application.Mapper;
using Shoppite.Application.Responses;
using Shoppite.Core.DTOs;
using Shoppite.Core.Model;
using Shoppite.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shoppite.Application.Handlers.CommandHandlers
{
    public class CreateOrdersHandler : IRequestHandler<CreateOrder, string>
    {
        private readonly IOrderRepository _orderRepository;
        public CreateOrdersHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<string> Handle(CreateOrder request, CancellationToken cancellationToken)
        {                 
             await _orderRepository.BuyNow(request.orders);
             return "Success";
        }
        
    }
}
