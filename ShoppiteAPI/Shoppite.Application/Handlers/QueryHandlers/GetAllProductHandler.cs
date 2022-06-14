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
    public class GetAllProductHandler : IRequestHandler<GetAllProductQuery, List<Core.DTOs.Product_DTO>>
    {
        private readonly IProductRepository _productRepo;

        public GetAllProductHandler(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }
        public async Task<List<Core.DTOs.Product_DTO>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            return (List<Core.DTOs.Product_DTO>)await _productRepo.GetproductNavList();
        }
    }
}
