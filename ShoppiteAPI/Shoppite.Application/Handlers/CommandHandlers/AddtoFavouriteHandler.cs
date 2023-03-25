using MediatR;
using Shoppite.Application.Commands;
using Shoppite.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shoppite.Application.Handlers.CommandHandlers
{
    public class AddtoFavouriteHandler: IRequestHandler<AddToFavourtite, string>
    {
        private readonly ICartRepository _cartRepository;
        public AddtoFavouriteHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public async Task<string> Handle(AddToFavourtite request, CancellationToken cancellationToken)
        {
            await _cartRepository.AddtoFavourite(request.favourites);
            return "Success";
        }
    }
}
