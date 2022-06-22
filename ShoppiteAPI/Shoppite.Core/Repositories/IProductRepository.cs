using Shoppite.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product_DTO>> GetproductNavList();
        Task<List<Product_DTO>> GetproductList(int category_id, int sub_category_id);
        Task<List<Product_DTO>> Getproductdisc(int category_id, int sub_category_id, int product_id);
        Task<CartProduct> PostCartProduct(CartProduct cartProduct);
        Task<List<CartProduct>> GetCartProduct(int org_id, int user_id);
        Task<WishList_DTO> PostWishList(WishList_DTO wishList_DTO);
        Task<List<WishList_DTO>> GetWishList(int org_id, int user_id);
        Task<List<WishList_DTO>> DeleteWishList(int org_id, int user_id, int id);
        Task<List<CartProduct>> DeleteCartList(int org_id, int user_id, int id);
    }
}
