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

namespace Shoppite.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Shoppite.Core.Entities.Category>, ICategoryRepository
    {
        public CategoryRepository(ShoppiteContext shoppiteContext) : base(shoppiteContext)
        {

        }
        public async Task<IEnumerable<Core.Entities.Category>> GetEmployeeByLastName(string lastname)
        {
            return await _shoppiteContext.Categories
                .Where(m => m.LastName == lastname)
                .ToListAsync();
        }
        public async Task<List<SubCategory_Category_DTO>> GetCategoryNavList()
        {
            GeneralDbContext generalDbContext = new GeneralDbContext();
            List<SubCategory_Category_DTO> subCategory_Category_DTO = new List<SubCategory_Category_DTO>();
            using (var connection = new SqlConnection(generalDbContext.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "proc_getallsub_categories_by_category";
                command.Parameters.AddWithValue("@org_id", 1);
                var dataReader = await command.ExecuteReaderAsync();
                ExtensionMethods extensionMethods = new ExtensionMethods();
                subCategory_Category_DTO = extensionMethods.DataReaderMapToList<SubCategory_Category_DTO>(dataReader);
                connection.Close();
                return subCategory_Category_DTO;
            }
        }
    }
}
