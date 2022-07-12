using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Application.Queries
{
    public record GetProductListQuery(int category_id, int sub_category_id) : IRequest<List<Core.DTOs.Product_DTO>>;
    public record GetProducDiscQuery(int category_id, int sub_category_id, int product_id) : IRequest<List<Core.DTOs.Product_DTO>>;
    public record GetAllCategoryQuery : IRequest<List<Core.DTOs.SubCategory_Category_DTO>>;
    public record GetAllCenterBannerQuery : IRequest<List<Core.DTOs.Center_Banner_DTO>>;
    public record GetAllCloth_subQuery : IRequest<List<Core.DTOs.Cloth_SubCategory_DTO>>;
    public record GetAllProductQuery : IRequest<List<Core.DTOs.Product_DTO>>;
    public record GetAllSidebarQuery : IRequest<List<Core.DTOs.Sidebar_DTO>>;
    public record GetAllSliderBannerQuery : IRequest<List<Core.DTOs.SliderBanner_DTO>>;
    public record GetAllCartProductQuery(int org_id, int user_id) : IRequest<List<Core.DTOs.CartProduct>>;
    public record GetAllWishListQuery(int org_id, int user_id) : IRequest<List<Core.DTOs.WishList_DTO>>;
    public record DeleteWishListQuery(int org_id, int user_id, int id) : IRequest<List<Core.DTOs.WishList_DTO>>;
    public record DeleteCartListQuery(int org_id, int user_id, int id) : IRequest<List<Core.DTOs.CartProduct>>;
    public record UpdateCartQuantityQuery(int org_id, int user_id, int id, int prod_quantity) :IRequest<List<Core.DTOs.CartProduct>>;
    public record GetAllUserQuery(int org_id, int id) :IRequest<List<Core.DTOs.UserInfo_DTO>>;
    public record GetAllCategoriesQuery(int org_id) : IRequest<List<Core.DTOs.Category_DTO>>;
    public record GetAllSubcategoryQuery(int org_id) : IRequest<List<Core.DTOs.Subcatgory_DTO>>;
    public record GetAllSubcategoryByCategoryQuery(int org_id, int category_id) : IRequest<List<Core.DTOs.Subcatgory_DTO>>;
    public record GetAllProductsQuery(int org_id) : IRequest<List<Core.DTOs.Product_DTO>>;
    public record DeleteCategoryQuery(int id, int org_id) :IRequest<List<Core.DTOs.Category_DTO>>;
    public record DeleteSubCategoryQuery(int id, int org_id) :IRequest<List<Core.DTOs.Subcatgory_DTO>>;
    public record DeleteProductQuery(int id, int org_id) :IRequest<List<Core.DTOs.Product_DTO>>;
    public record UpdateCategorytQuery(int id, int org_id, string category_code, string category_name, string category_description, string category_image) : IRequest<List<Core.DTOs.Category_DTO>>;
    public record GetCategorybyidQuery(int id) :IRequest<List<Core.DTOs.Category_DTO>>;
    public record UpdateSubCategoryQuery(int id, int org_id, int category_id, string sub_ctg_name, string sub_ctg_description, string sub_ctg_code, string sub_ctg_image) : IRequest<List<Core.DTOs.Subcatgory_DTO>>;
}
