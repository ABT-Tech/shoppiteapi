using MediatR;
using Shoppite.Application.Queries;
using Shoppite.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shoppite.Application.Handlers.QueryHandlers
{
    public class UpdateCartQuantityHandler : IRequestHandler<UpdateCartQuantityQuery, List<Core.DTOs.CartProduct>>
    {
        private readonly IProductRepository _productRepository;

        public UpdateCartQuantityHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<Core.DTOs.CartProduct>> Handle(UpdateCartQuantityQuery request, CancellationToken cancellationToken)
        {
            return (List<Core.DTOs.CartProduct>)await _productRepository.UpdateCartQuantity(request.org_id, request.user_id, request.id, request.prod_quantity);
        }
    }
}
