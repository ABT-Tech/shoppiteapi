using MediatR;
using Shoppite.Application.Commands;
using Shoppite.Application.Mapper;
using Shoppite.Application.Responses;
using Shoppite.Core.DTOs;
using Shoppite.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shoppite.Application.Handlers.CommandHandlers
{
    public class CreateWishListHandler : IRequestHandler<CreateWishListCommand, WishList_DTO>
    {
        private readonly IProductRepository _createProductRepository;

        public CreateWishListHandler(IProductRepository createProductRepository)
        {
            _createProductRepository = createProductRepository;
        }
        public async Task<WishList_DTO> Handle(CreateWishListCommand request, CancellationToken cancellationToken)
        {
            return await _createProductRepository.PostWishList(request.WishList);
        }
    }
}
