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
    public class SearchProductHandler: IRequestHandler<SearchProduct, List<ProductResponse>>
    {
        private readonly IProductRepository _ProductRepository;

        public SearchProductHandler(IProductRepository productRepository)
        {
            _ProductRepository = productRepository;
        }
        public async Task<List<ProductResponse>> Handle(SearchProduct request, CancellationToken cancellationToken)
        {
            var products = await _ProductRepository.SearchProducts(request.org_id,request.productname,request.OrgCategoryId);
            var mapper = ObjectMapper.Mapper.Map<List<ProductResponse>>(products);
            return mapper;
        }

    }
   
}
