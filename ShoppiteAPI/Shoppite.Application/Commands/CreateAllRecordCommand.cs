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
    public record CreateOrderCommand(List<CartProduct>  order) : IRequest<Order_DTO>;
    public record CreateVendorCommand(Vendor_DTO vendor) : IRequest<Vendor_DTO>;
    public record CreateProductCommand(Product_DTO product_DTO) : IRequest<Product_DTO>;
    public record CreateCategoriesCommand(Category_DTO category_DTO) : IRequest<Category_DTO>;
    public record CreateSubcategoryCommand(Subcatgory_DTO subcatgory_DTO) : IRequest<Subcatgory_DTO>;
    public record CreateVendorUsersCommand(Vendor_Users_DTO vendorusers) : IRequest<Vendor_Users_DTO>;
}
