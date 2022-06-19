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
    public record CreateAuthCommand(UserCredential UserCredentials) : IRequest<string>;
}
