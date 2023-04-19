using Shoppite.Application.Queries;
using Shoppite.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Shoppite.Application.Responses;
using Shoppite.Application.Mapper;

namespace Shoppite.Application.Handlers.QueryHandlers
{
    public class GetAllProductsByOrganizationsHandler : IRequestHandler<GetAllProductsByOrganizationsQuery, List<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsByOrganizationsHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<ProductResponse>> Handle(GetAllProductsByOrganizationsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllProductsByOrganizations(request.org_id);
            var mapper = ObjectMapper.Mapper.Map<List<ProductResponse>>(products);
            return mapper;
        }
    }
}
