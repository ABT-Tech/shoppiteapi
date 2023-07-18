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
    public record RemovefromFavourite(int ProductId,int UserId, int OrgId, int? SpecificationId) : IRequest<string>;
    public record UserRegistration(UserRegistrationDTO RegistrationDTO) : IRequest<string>;
    public record EditUserProfile(UserDTO UserDTO):IRequest<string>;
    public record AddFirebaseToken(FireBaseToken fireBaseToken) : IRequest<string>;
    public record UpdateOrderStatus(Orders orders):IRequest<string>;
    public record RemoveFromCart(int userid, int proid, int orgid,int? SpecificationId) : IRequest<string>;
    public record UpdateNotificationStatus(int NotificationId) : IRequest<string>;
    public record CancelOrder(CancelOrders cancelOrder):IRequest<string>;
    public record Forgetpassword(ForgetPassword Password):IRequest<string>;
    public record UpdateProductDetails(UpdateProductDetail Products):IRequest<string>;
    public record UpdateUserStatus(CustomerInfo cinfo):IRequest<string>;
}
