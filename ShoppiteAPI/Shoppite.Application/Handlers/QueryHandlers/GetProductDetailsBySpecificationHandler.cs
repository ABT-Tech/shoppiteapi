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
    public class GetProductDetailsBySpecificationHandler : IRequestHandler<GetProductDetailsBySpecification, List<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;
        public GetProductDetailsBySpecificationHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<ProductResponse>> Handle(GetProductDetailsBySpecification request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetProductDetailsBySpecification(request.OrgId, request.ProductGUId,request.SpecificationId,request.userId);
            var mapper = ObjectMapper.Mapper.Map<List<ProductResponse>>(products);
            return mapper;
        }
    }
}
