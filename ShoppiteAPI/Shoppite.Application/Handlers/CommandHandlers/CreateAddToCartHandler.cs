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
    public class CreateAddToCartHandler : IRequestHandler<CreateAddToCartCommand, CartProduct>
    {
        private readonly IProductRepository _createProductRepository;

        public CreateAddToCartHandler(IProductRepository createProductRepository)
        {
            _createProductRepository = createProductRepository;
        }
        public async Task<CartProduct> Handle(CreateAddToCartCommand request, CancellationToken cancellationToken)
        {
            return await _createProductRepository.PostCartProduct(request.Product); 
        }
    }
}
