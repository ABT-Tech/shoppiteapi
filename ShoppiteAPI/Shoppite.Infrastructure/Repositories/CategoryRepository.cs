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
            return await _MasterContext.CategoryMasters.Where(x => x.OrgId == OrgId).ToListAsync();

        }
    }
}
