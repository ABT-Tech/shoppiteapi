using Shoppite.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.Repositories
{
    public interface ISubcategoryRepository
    {
        Task<Subcatgory_DTO> PostSubcategory(Subcatgory_DTO subcatgory_DTO);
        Task<List<Subcatgory_DTO>> GetAllSubcategory(int org_id);
        Task<List<Subcatgory_DTO>> GetAllSubcategoryByCategory(int org_id, int category_id);
        Task<List<Subcatgory_DTO>> DeleteSubcategory(int id, int org_id);
        Task<List<Subcatgory_DTO>> UpdateSubCategory(int id, int org_id, int category_id, string sub_ctg_name, string sub_ctg_description, string sub_ctg_code, string sub_ctg_image);
        Task<List<Subcatgory_DTO>> GetSubCategorybyid(int id, int org_id);
    }
}
