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
    public class DeleteCartListHandler : IRequestHandler<DeleteCartListQuery, List<Core.DTOs.CartProduct>>
    {
        private readonly IProductRepository _productRepository;

        public DeleteCartListHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<Core.DTOs.CartProduct>> Handle(DeleteCartListQuery request, CancellationToken cancellationToken)
        {
            return (List<Core.DTOs.CartProduct>)await _productRepository.DeleteCartList(request.org_id, request.user_id, request.id);
        }
    }
}
