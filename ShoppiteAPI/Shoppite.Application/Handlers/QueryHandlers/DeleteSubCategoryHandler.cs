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
    public class DeleteSubCategoryHandler : IRequestHandler<DeleteSubCategoryQuery, List<Core.DTOs.Subcatgory_DTO>>
    {
        private readonly ISubcategoryRepository _productRepository;

        public DeleteSubCategoryHandler(ISubcategoryRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<Core.DTOs.Subcatgory_DTO>> Handle(DeleteSubCategoryQuery request, CancellationToken cancellationToken)
        {
            return (List<Core.DTOs.Subcatgory_DTO>)await _productRepository.DeleteSubcategory(request.id, request.org_id);
        }
    }
}
