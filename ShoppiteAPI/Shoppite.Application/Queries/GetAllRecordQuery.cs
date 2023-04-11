using MediatR;
using Shoppite.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Application.Queries
{
    public record GetAllCategoriesQuery(int org_id) : IRequest<List<CategoryResponse>>;
    public record GetAllCarosolCategories(int org_id) : IRequest<List<CategoryResponse>>;
    public record GetAllOrganizationQuery() : IRequest<List<OrganizationResponse>>;
    public record GetAllProductsByOrganizationsQuery(int org_id) : IRequest<List<ProductResponse>>;
    public record GetWishlistByUserQuery(int org_id, int user_id) : IRequest<List<ProductResponse>>;
    public record GetMostSellerProductsByOrganizationsQuery(int org_id) : IRequest<List<ProductResponse>>;
    public record GetLastVisitedProductsByOrganizationsQuery(int org_id) : IRequest<List<ProductResponse>>;
    public record GetCartDetailsQuery(int org_id,int UserId) : IRequest<List<CartResponse>>;
    public record GetMyOrderDetailsQuery(int org_id,int UserId):IRequest<List<MyOrderResponse>>;
    public record GetUserByIdQuery(int org_id, int user_id) : IRequest<List<UserResponse>>;
    public record GetAddressByUserid(int org_id, int UserId) : IRequest<List<AddressResponse>>;
    public record SearchProduct(int org_id,string productname):IRequest<List<ProductResponse>>;
    public record GetRecentlyViewedProductsByCategory(int OrgId,int CategoryId,string IP):IRequest<List<RecentlyViewedProductResponse>>;
    public record GetMostViewedProductsByCategory(int OrgId, int CategoryId, string IP) : IRequest<List<RecentlyViewedProductResponse>>;
    public record GetProductsByBestSellers(int OrgId):IRequest<List<ProductsByBestSellerResponse>>;
    public record GetOrdersDetailByOrgId(int OrgId,int OrderMasterId) :IRequest<OrderDetailResponse>;
    public record GetOrderDetails(int OrgId):IRequest<List<VendorsOrderResponse>>;

}
