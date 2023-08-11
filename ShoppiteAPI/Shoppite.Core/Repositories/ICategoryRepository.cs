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
    public interface ICategoryRepository 
    {
        //custom operations here
        Task<List<CategoryDTO>> GetAllCategory(int OrgId);
    }
}
