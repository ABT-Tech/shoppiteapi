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
    public record GetUserByIdQuery(int org_id, int user_id) : IRequest<List<UserResponse>>;
}
