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
    public class GetParentCategoriesHandler : IRequestHandler<GetAllParentCategories, List<MainCategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetParentCategoriesHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<List<MainCategoryResponse>> Handle(GetAllParentCategories request, CancellationToken cancellationToken)
        {
            var mainCategories = await _categoryRepository.GetAllParentcategories();
            var mapper = ObjectMapper.Mapper.Map<List<MainCategoryResponse>>(mainCategories);
            return mapper;
        }
    }
}
