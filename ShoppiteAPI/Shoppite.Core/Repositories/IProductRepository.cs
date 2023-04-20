using Shoppite.Core.DTOs;
using Shoppite.Core.Model;
using Shoppite.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.Repositories
{
    public interface IProductRepository
    {
        Task<List<ProductsDTO>> GetAllProductsByOrganizations(int orgId,int? UserId);
        Task<List<ProductsDTO>> GetWishlistByUser(int orgId, int user_id);
        Task<List<ProductsDTO>> SearchProducts(int orgId,string productname);
        Task<List<RecentlyViewedProductDTO>> GetRecentlyViewedProductsByCategory(int OrgId,int CategoryId,string IP);
        Task<List<RecentlyViewedProductDTO>> MostViewedProductsByCategory(int OrgId, int CategoryId, string IP);
        Task<List<ProductsByBestSellerDTO>> ProductByBestSellers(int OrgId);
        Task<List<ProductsDTO>> GetProductsByCategory(int OrgId, int CategoryId);


    }
}
