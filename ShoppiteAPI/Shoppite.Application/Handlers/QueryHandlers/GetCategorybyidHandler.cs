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
    public class GetCategorybyidHandler : IRequestHandler<GetCategorybyidQuery, List<Core.DTOs.Category_DTO>>
    {
        private readonly ICategoriesRepository _productRepository;

        public GetCategorybyidHandler(ICategoriesRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<Core.DTOs.Category_DTO>> Handle(GetCategorybyidQuery request, CancellationToken cancellationToken)
        {
            return (List<Core.DTOs.Category_DTO>)await _productRepository.GetCategorybyid(request.id);
        }
    }
}
