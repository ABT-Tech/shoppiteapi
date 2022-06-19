using Shoppite.Application.Queries;
using Shoppite.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shoppite.Application.Handlers.QueryHandlers
{
    public class GetAllCartProductHandler : IRequestHandler<GetAllCartProductQuery, List<Core.DTOs.CartProduct>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllCartProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<Core.DTOs.CartProduct>> Handle(GetAllCartProductQuery request, CancellationToken cancellationToken)
        {
            return (List<Core.DTOs.CartProduct>)await _productRepository.GetCartProduct(request.org_id,request.user_id);
        }
    }
}
