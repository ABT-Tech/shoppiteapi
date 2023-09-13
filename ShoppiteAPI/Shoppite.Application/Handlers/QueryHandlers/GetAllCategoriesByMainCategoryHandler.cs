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
   public class GetAllCategoriesByMainCategoryHandler : IRequestHandler<GetAllSubCategories, List<CategoriesResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;
        public GetAllCategoriesByMainCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<List<CategoriesResponse>> Handle(GetAllSubCategories request, CancellationToken cancellationToken)
        {
            var Categories = await _categoryRepository.GetAllSubCategories();
            var mapper = ObjectMapper.Mapper.Map<List<CategoriesResponse>>(Categories);
            return mapper;
        }
    }
}
