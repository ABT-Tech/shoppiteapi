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
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, List<Core.DTOs.Category_DTO>>
    {
        private readonly ICategoriesRepository _categoryRepository;

        public GetAllCategoriesHandler(ICategoriesRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<List<Core.DTOs.Category_DTO>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            return (List<Core.DTOs.Category_DTO>)await _categoryRepository.GetAllCategory(request.org_id);
        }
    }
}
