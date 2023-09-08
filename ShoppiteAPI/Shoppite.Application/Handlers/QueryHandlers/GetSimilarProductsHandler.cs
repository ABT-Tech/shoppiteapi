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
    public class GetSimilarProductsHandler : IRequestHandler<GetSimilarProducts, List<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        public GetSimilarProductsHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<ProductResponse>> Handle(GetSimilarProducts request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetSimilarProducts(request.OrgId, request.CategoryId,request.BrandId,request.UserId,request.OrgCategoryId);
            var mapper = ObjectMapper.Mapper.Map<List<ProductResponse>>(products);
            return mapper;
        }
    }
}
