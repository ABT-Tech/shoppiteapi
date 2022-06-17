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
    }
}
