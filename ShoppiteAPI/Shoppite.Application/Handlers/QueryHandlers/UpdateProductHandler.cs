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
    public class UpdateProductHandler : IRequestHandler<UpdateCategorytQuery, List<Core.DTOs.Category_DTO>>
    {
        private readonly ICategoriesRepository _productRepository;

        public UpdateProductHandler(ICategoriesRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<Core.DTOs.Category_DTO>> Handle(UpdateCategorytQuery request, CancellationToken cancellationToken)
        {
            return (List<Core.DTOs.Category_DTO>)await _productRepository.UpdateCategory(request.id,request.org_id,request.category_code,request.category_name,request.category_description,request.category_image);
        }
    }
}
