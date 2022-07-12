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
    public class UpdateSubCategoryHandler : IRequestHandler<UpdateSubCategoryQuery, List<Core.DTOs.Subcatgory_DTO>>
    {
        private readonly ISubcategoryRepository _productRepository;

        public UpdateSubCategoryHandler(ISubcategoryRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<Core.DTOs.Subcatgory_DTO>> Handle(UpdateSubCategoryQuery request, CancellationToken cancellationToken)
        {
            return (List<Core.DTOs.Subcatgory_DTO>)await _productRepository.UpdateSubCategory(request.id, request.org_id, request.category_id, request.sub_ctg_name, request.sub_ctg_description, request.sub_ctg_code, request.sub_ctg_image);
        }
    }
}
