using Shoppite.Core.DTOs;
using Shoppite.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.Repositories
{
    public interface ICategoryRepository : IRepository<Shoppite.Core.Entities.Category>
    {
        //custom operations here
        Task<IEnumerable<Shoppite.Core.Entities.Category>> GetEmployeeByLastName(string lastname);
        Task<List<SubCategory_Category_DTO>> GetCategoryNavList();
    }
}
