using MediatR;
//using Shoppite.Application.Responses;
using Shoppite.Core.DTOs;
using Shoppite.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Application.Commands
{
    public record CreateAuthCommand(UserCredential UserCredentials) : IRequest<Users_DTO>;
    public record AddToCartCommand(CartRequest Cart) : IRequest<string>;
    public record CreateOrder(OrdersDTO orders):IRequest<string>;
    public record AddToFavourtite(Favourite favourites):IRequest<string>;
}
