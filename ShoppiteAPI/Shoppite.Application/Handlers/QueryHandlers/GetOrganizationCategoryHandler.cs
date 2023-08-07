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
    public class GetOrganizationCategoryHandler : IRequestHandler<GetAllOrganizationCategoryQuery, List<OrganizationCategoryResponse>>
    {
        private readonly IOrganizationRepository _organizationRepository;

        public GetOrganizationCategoryHandler(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }
        public async Task<List<OrganizationCategoryResponse>> Handle(GetAllOrganizationCategoryQuery request, CancellationToken cancellationToken)
        {
            var organizations = await _organizationRepository.GetOrganizationCategories();
            var mapper = ObjectMapper.Mapper.Map<List<OrganizationCategoryResponse>>(organizations);
            return mapper;
        }
    }
}
