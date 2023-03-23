using Shoppite.Application.Queries;
using Shoppite.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Shoppite.Application.Responses;
using Shoppite.Application.Mapper;

namespace Shoppite.Application.Handlers.QueryHandlers
{
     public class GetWishlistByUserHandler : IRequestHandler<GetWishlistByUserQuery, List<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;
        
        public GetWishlistByUserHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<ProductResponse>> Handle(GetWishlistByUserQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetWishlistByUser(request.org_id, request.user_id);
            var mapper = ObjectMapper.Mapper.Map<List<ProductResponse>>(products);
            return mapper;
        }
    }
}
