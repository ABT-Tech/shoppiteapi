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
    public class RemoveFromCartHandler:IRequestHandler<RemoveFromCart, string>
    {
        private readonly ICartRepository _cartRepository;

        public RemoveFromCartHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public async Task<string> Handle(RemoveFromCart request, CancellationToken cancellationToken)
        {
            await _cartRepository.RemoveFromCart(request.userid, request.proid, request.orgid,request.SpecificationId);
            return "Success";
        }
    }
}
