using MediatR;
using Shoppite.Application.Responses;
using Shoppite.Core.DTOs;
using System;
using System.Collections.Generic;

namespace Shoppite.Application.Queries
{
    public record GetAllCategoriesQuery(int org_id) : IRequest<List<CategoryResponse>>;
    public record GetAllCarosolCategories(int org_id) : IRequest<List<CategoryResponse>>;
    public record GetAllOrganizationQuery(int Org_CategoryId) : IRequest<List<OrganizationResponse>>;
    public record GetAllProductsByOrganizationsQuery(int org_id,int? userId) : IRequest<List<ProductResponse>>;
    public record GetWishlistByUserQuery(int org_id, int user_id) : IRequest<List<ProductResponse>>;
    public record GetMostSellerProductsByOrganizationsQuery(int org_id) : IRequest<List<ProductResponse>>;
    public record GetLastVisitedProductsByOrganizationsQuery(int org_id) : IRequest<List<ProductResponse>>;
    public record GetCartDetailsQuery(int org_id,int UserId) : IRequest<List<CartResponse>>;
    public record GetMyOrderDetailsQuery(int org_id,int UserId):IRequest<List<MyOrderResponse>>;
    public record GetUserByIdQuery(int org_id, int user_id) : IRequest<List<UserResponse>>;
    public record GetAddressByUserid(int org_id, int UserId) : IRequest<List<AddressResponse>>;
    public record SearchProduct(int org_id,string productname):IRequest<List<ProductResponse>>;
    public record GetRecentlyViewedProductsByCategory(int OrgId,string IP):IRequest<List<RecentlyViewedProductResponse>>;
    public record GetMostViewedProductsByCategory(int OrgId, int CategoryId, string IP) : IRequest<List<RecentlyViewedProductResponse>>;
    public record GetProductsByBestSellers(int OrgId,string Type):IRequest<List<ProductsByBestSellerResponse>>;
    public record GetOrdersDetailByOrgId(int OrgId,int OrderMasterId) :IRequest<OrderDetailResponse>;
    public record GetOrderDetails(int OrgId):IRequest<List<VendorsOrderResponse>>;
    public record GetProductsByCategory(int OrgId,int CategoryId):IRequest<List<ProductResponse>>;
    public record GetNotificationDetails(int NotificationID) : IRequest<List<NotificationsDataDTO>>;
    public record GetDeviceListToSendNotifications(string Type, int UserID) : IRequest<List<DeviceListDTO>>;
    public record GetAllProductForVendor(int OrgId,int Id): IRequest<ProductDetailsForVendorResponse>;
    public record GetCustomerDetails(int OrgId) : IRequest<List<CustomerInfoResponse>>;
    public record GetTotalOrderDetails(int OrgId):IRequest<List<ReportResponse>>;
    public record GetSimilarProducts(int OrgId, int CategoryId,int BrandId) : IRequest<List<ProductResponse>>;
    public record GetNumOfItemsInCart(int OrgId,int UserId):IRequest<NumberOfCartItemResponse>;
    public record GetProductVariationQuery(int OrgId, Guid ProductGUId) : IRequest<List<ProductVariationResponse>>;
    public record GetProductDetailsBySpecification(int OrgId, Guid ProductGUId,int? SpecificationId, int? userId) : IRequest<List<ProductResponse>>;
    public record GetOnePayFlag(int OrgId) : IRequest<OnePayFlagResponse>;
    public record GetAllOrganizationCategoryQuery() : IRequest<List<OrganizationCategoryResponse>>;
}
