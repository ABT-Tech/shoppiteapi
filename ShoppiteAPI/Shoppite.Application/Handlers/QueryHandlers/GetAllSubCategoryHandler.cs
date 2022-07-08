using MediatR;
using Shoppite.Application.Queries;
using Shoppite.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shoppite.Application.Handlers.QueryHandlers
{
    public class GetAllSubCategoryHandler : IRequestHandler<GetAllSubcategoryQuery, List<Core.DTOs.Subcatgory_DTO>>
    {
        private readonly ISubcategoryRepository _subcategoryRepository;

        public GetAllSubCategoryHandler(ISubcategoryRepository subcategoryRepository)
        {
            _subcategoryRepository = subcategoryRepository;
        }
        public async Task<List<Core.DTOs.Subcatgory_DTO>> Handle(GetAllSubcategoryQuery request, CancellationToken cancellationToken)
        {
            return (List<Core.DTOs.Subcatgory_DTO>)await _subcategoryRepository.GetAllSubcategory(request.org_id);
        }
    }
}
