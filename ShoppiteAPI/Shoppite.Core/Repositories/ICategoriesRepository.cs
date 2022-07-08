using Shoppite.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.Repositories
{
    public interface ICategoriesRepository
    {
        Task<Category_DTO> PostCategory(Category_DTO category_DTO);
        Task<List<Category_DTO>> GetAllCategory(int org_id);
        Task<List<Category_DTO>> DeleteCategory(int id, int org_id);
        Task<List<Category_DTO>> UpdateCategory( int id, int org_id, string category_code, string category_name, string category_description, string category_image);
        Task<List<Category_DTO>> GetCategorybyid(int id);
    }
}
