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

}
