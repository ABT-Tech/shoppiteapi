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
    public class GetProductVariationHandler : IRequestHandler<GetProductVariationQuery, List<ProductVariationResponse>>
    {
        private readonly IProductRepository _productRepository;
        public GetProductVariationHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<ProductVariationResponse>> Handle(GetProductVariationQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetProductVariationDetail(request.OrgId, request.ProductGUId);
            var mapper = ObjectMapper.Mapper.Map<List<ProductVariationResponse>>(products);
            return mapper;
        }
    }
}
