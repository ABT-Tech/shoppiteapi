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
    public class DeleteProductHandler : IRequestHandler<DeleteProductQuery, List<Core.DTOs.Product_DTO>>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<Core.DTOs.Product_DTO>> Handle(DeleteProductQuery request, CancellationToken cancellationToken)
        {
            return (List<Core.DTOs.Product_DTO>)await _productRepository.DeleteProduct(request.id, request.org_id);
        }
    }
}
