using Shoppite.Core.Repositories;
using Shoppite.Infrastructure.Data;
using Shoppite.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;
using Shoppite.Core.DTOs;
using Shoppite.Core.Extensions;
using Shoppite.Core.Entities;
using Shoppite.Core.Model;

namespace Shoppite.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        protected readonly Shoppite_masterContext _MasterContext;
        public CategoryRepository(Shoppite_masterContext dbContext)
        {
            _MasterContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<List<CategoryMaster>> GetAllCategory(int OrgId) 
        {
            var categoryList = from c in _MasterContext.CategoryMasters
                               join p in _MasterContext.ProductCategories on c.CategoryId equals p.CategoryId
                               where c.OrgId == OrgId && c.ParentCategoryId != 0
                               select c;
            return await categoryList.Distinct().ToListAsync();

        }
    }
}
