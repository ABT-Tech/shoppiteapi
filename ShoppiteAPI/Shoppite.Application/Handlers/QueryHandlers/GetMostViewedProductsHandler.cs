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
    public class GetMostViewedProductsHandler:IRequestHandler<GetMostViewedProductsByCategory, List<RecentlyViewedProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        public GetMostViewedProductsHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<RecentlyViewedProductResponse>> Handle(GetMostViewedProductsByCategory request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.MostViewedProductsByCategory(request.OrgId, request.CategoryId, request.IP);
            var mapper = ObjectMapper.Mapper.Map<List<RecentlyViewedProductResponse>>(products);
            return mapper;
        }
    }
}
