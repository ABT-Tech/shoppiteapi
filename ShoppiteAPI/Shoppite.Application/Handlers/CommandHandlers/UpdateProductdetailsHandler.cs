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
    public class UpdateProductdetailsHandler : IRequestHandler<UpdateProductDetails, string>
    {
        private readonly IProductRepository _productRepository;
        public UpdateProductdetailsHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<string> Handle(UpdateProductDetails request, CancellationToken cancellationToken)
        {
            return await _productRepository.UpdateProductDetailsForVendor(request.Products);
        }
    }
}
