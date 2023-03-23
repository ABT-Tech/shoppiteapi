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
    public class AddToCartHandler : IRequestHandler<AddToCartCommand, string>
    {
        private readonly ICartRepository _cart;

        public AddToCartHandler(ICartRepository cart)
        {
            _cart = cart;
        }
        public async Task<string> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            return await _cart.AddToCart(request.Cart); 
        }
    }
}
