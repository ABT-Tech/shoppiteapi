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
    public record CreateOrder(OrdersDTO orders):IRequest<string>;
    public record AddToFavourtite(Favourite favourites):IRequest<string>;
    public record RemovefromFavourite(int ProductId,int UserId,int OrgId) : IRequest<int>;
    public record UserRegistration(UserRegistrationDTO RegistrationDTO) : IRequest<string>;

}
