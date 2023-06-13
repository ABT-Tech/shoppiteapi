using MediatR;
using Shoppite.Application.Mapper;
using Shoppite.Application.Queries;
using Shoppite.Application.Responses;
using Shoppite.Core.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Shoppite.Application.Handlers.QueryHandlers
{
    public class GetAllProductsForVendorHandler : IRequestHandler<GetAllProductForVendor, ProductDetailsForVendorResponse>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsForVendorHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ProductDetailsForVendorResponse> Handle(GetAllProductForVendor request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllProductsForVendor(request.OrgId,request.Id);
            var mapper = ObjectMapper.Mapper.Map<ProductDetailsForVendorResponse>(products);
            return mapper;
        }
    }
}
