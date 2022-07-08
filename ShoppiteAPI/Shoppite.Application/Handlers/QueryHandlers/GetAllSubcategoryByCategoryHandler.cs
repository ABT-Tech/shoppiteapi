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
    public class GetAllSubcategoryByCategoryHandler : IRequestHandler<GetAllSubcategoryByCategoryQuery, List<Core.DTOs.Subcatgory_DTO>>
    {
        private readonly ISubcategoryRepository _subcategoryRepo;

        public GetAllSubcategoryByCategoryHandler(ISubcategoryRepository subcategoryRepo)
        {
            _subcategoryRepo = subcategoryRepo;
        }
        public async Task<List<Core.DTOs.Subcatgory_DTO>> Handle(GetAllSubcategoryByCategoryQuery request, CancellationToken cancellationToken)
        {
            return (List<Core.DTOs.Subcatgory_DTO>)await _subcategoryRepo.GetAllSubcategoryByCategory(request.org_id,request.category_id);
        }
    }
}
