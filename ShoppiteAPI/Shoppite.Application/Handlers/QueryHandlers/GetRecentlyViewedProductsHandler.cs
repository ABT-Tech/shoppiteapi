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
    class GetRecentlyViewedProductsHandler : IRequestHandler<GetRecentlyViewedProductsByCategory, List<RecentlyViewedProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        public GetRecentlyViewedProductsHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<RecentlyViewedProductResponse>> Handle(GetRecentlyViewedProductsByCategory request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetRecentlyViewedProductsByCategory(request.OrgId,request.IP);
            var mapper = ObjectMapper.Mapper.Map<List<RecentlyViewedProductResponse>>(products);
            return mapper;
        }
    }
}
