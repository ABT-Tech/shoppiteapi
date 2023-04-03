using AutoMapper;
using MediatR;
using Shoppite.Application.Mapper;
using Shoppite.Application.Queries;
using Shoppite.Application.Responses;
using Shoppite.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shoppite.Application.Handlers.QueryHandlers
{
    public class GetProductsByBestSellersHandler : IRequestHandler<GetProductsByBestSellers, List<ProductsByBestSellerResponse>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsByBestSellersHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<ProductsByBestSellerResponse>> Handle(GetProductsByBestSellers request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.ProductByBestSellers(request.OrgId);
            var mapper = ObjectMapper.Mapper.Map<List<ProductsByBestSellerResponse>>(products);
            return mapper;
        }
    }
}
