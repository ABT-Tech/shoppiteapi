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
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, List<CategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepo;

        public GetAllCategoriesHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepo = categoryRepository;
        }
        public async Task<List<CategoryResponse>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var CategoryList = await _categoryRepo.GetAllCategory(request.org_id);
            var mapper = ObjectMapper.Mapper.Map<List<CategoryResponse>>(CategoryList);
            return mapper;
        }
    }
}
