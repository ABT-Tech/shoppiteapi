using MediatR;
using Shoppite.Application.Commands;
using Shoppite.Core.DTOs;
using Shoppite.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shoppite.Application.Handlers
{
    public class CreateCategoriesHandler : IRequestHandler<CreateCategoriesCommand, Category_DTO>
    {
        private readonly ICategoriesRepository _createCategoryRepository;

        public CreateCategoriesHandler(ICategoriesRepository createCategoryRepository)
        {
            _createCategoryRepository = createCategoryRepository;
        }
        public async Task<Category_DTO> Handle(CreateCategoriesCommand request, CancellationToken cancellationToken)
        {
            return await _createCategoryRepository.PostCategory(request.category_DTO);
        }
    }
}
