using MediatR;
using Shoppite.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Application.Commands
{
    public record CreateAddToCartCommand(CartProduct Product) : IRequest<CartProduct>;
    public record CreateAuthCommand(UserCredential UserCredentials) : IRequest<Users_DTO>;
    public record CreateWishListCommand(WishList_DTO WishList) : IRequest<WishList_DTO>;
    public record CreateUserSignupCommand(Users_DTO user) : IRequest<Users_DTO>;
}
