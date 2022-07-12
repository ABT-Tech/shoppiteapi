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

namespace Shoppite.Application.Handlers
{
    public class UpdateCategoryHandler : IRequestHandler<CreateProductCommand, Product_DTO>
    {
        private readonly IProductRepository _createProductRepository;

        public UpdateCategoryHandler(IProductRepository createProductRepository)
        {
            _createProductRepository = createProductRepository;
        }
        public async Task<Product_DTO> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            return await _createProductRepository.PostProduct(request.product_DTO);
        }
    }
}
