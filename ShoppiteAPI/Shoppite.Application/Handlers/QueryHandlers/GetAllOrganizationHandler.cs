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
    public class GetAllOrganizationHandler : IRequestHandler<GetAllOrganizationQuery, List<OrganizationResponse>>
    {
        private readonly IOrganizationRepository _organizationRepository;

        public GetAllOrganizationHandler(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }
        public async Task<List<OrganizationResponse>> Handle(GetAllOrganizationQuery request, CancellationToken cancellationToken)
        {
            var organizations = await _organizationRepository.GetOrganizations();
            var mapper = ObjectMapper.Mapper.Map<List<OrganizationResponse>>(organizations);
            return mapper;
        }
    }
}
