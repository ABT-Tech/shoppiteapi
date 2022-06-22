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
    public class GetAllWishListHandler : IRequestHandler<GetAllWishListQuery, List<Core.DTOs.WishList_DTO>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllWishListHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<Core.DTOs.WishList_DTO>> Handle(GetAllWishListQuery request, CancellationToken cancellationToken)
        {
            return (List<Core.DTOs.WishList_DTO>)await _productRepository.GetWishList(request.org_id, request.user_id);
        }
    }
}
