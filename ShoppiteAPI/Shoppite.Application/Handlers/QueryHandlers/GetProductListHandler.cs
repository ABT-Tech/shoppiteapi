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
    public class GetProductListHandler : IRequestHandler<GetProductListQuery, List<Core.DTOs.Product_DTO>>
    {
        private readonly IProductRepository _productRepo;

        public GetProductListHandler(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }
        public async Task<List<Core.DTOs.Product_DTO>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            return (List<Core.DTOs.Product_DTO>)await _productRepo.GetproductList(request.category_id, request.sub_category_id);
        }
    }
}
