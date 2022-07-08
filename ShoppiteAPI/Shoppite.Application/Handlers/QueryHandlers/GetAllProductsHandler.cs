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
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, List<Core.DTOs.Product_DTO>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<Core.DTOs.Product_DTO>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return (List<Core.DTOs.Product_DTO>)await _productRepository.GetAllProduct(request.org_id);
        }
    }
}
