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
    public class GetProductsByCategoryHandler:IRequestHandler<GetProductsByCategory, List<ProductsByCategoryResponse>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsByCategoryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<ProductsByCategoryResponse>> Handle(GetProductsByCategory request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetProductsByCategory(request.OrgId,request.CategoryId);
            var mapper = ObjectMapper.Mapper.Map<List<ProductsByCategoryResponse>>(products);
            return mapper;
        }
    }
}
