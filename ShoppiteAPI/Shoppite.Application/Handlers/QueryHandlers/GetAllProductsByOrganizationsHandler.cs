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
    public class GetAllProductsByOrganizationsHandler : IRequestHandler<GetAllProductsByOrganizationsQuery, ProductMasterResponse>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsByOrganizationsHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ProductMasterResponse> Handle(GetAllProductsByOrganizationsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllProductsByOrganizations(request.org_id,request.userId,request.orgcat_Id);
            var mapper = ObjectMapper.Mapper.Map<ProductMasterResponse>(products);
            return mapper;
        }
    }
}
