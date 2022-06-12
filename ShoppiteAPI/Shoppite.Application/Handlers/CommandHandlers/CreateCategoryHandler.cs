using Shoppite.Application.Commands;
using Shoppite.Application.Mapper;
using Shoppite.Application.Responses;
using Shoppite.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shoppite.Application.Handlers.CommandHandlers
{

    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, CategoryResponse>
    {
        private readonly ICategoryRepository _createCategoryRepo;

        public CreateCategoryHandler(ICategoryRepository categoryRepository)
        {
            _createCategoryRepo = categoryRepository;
        }
        public async Task<CategoryResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryEntitiy = CategoryMapper.Mapper.Map<Shoppite.Core.Entities.Category>(request);
            if(categoryEntitiy is null)
            {
                throw new ApplicationException("Issue with mapper");
            }
            var newCategory = await _createCategoryRepo.AddAsync(categoryEntitiy);
            var categoryResponse = CategoryMapper.Mapper.Map<CategoryResponse>(newCategory);
            return categoryResponse;
        }
    }
}
