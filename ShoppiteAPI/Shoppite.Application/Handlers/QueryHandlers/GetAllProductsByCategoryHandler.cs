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
    class GetAllProductsByCategoryHandler : IRequestHandler<GetAllProductsByCategoryQuery, List<AllProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsByCategoryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<AllProductResponse>> Handle(GetAllProductsByCategoryQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllProductsByCategory(request.OrgId, request.OrgCategoryId,request.CategoryId);
            var mapper = ObjectMapper.Mapper.Map<List<AllProductResponse>>(products);
            return mapper;
        }
    }
}
