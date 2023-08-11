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
        public async Task<List<CategoryDTO>> GetAllCategory(int OrgId) 
        {
            List<CategoryDTO> categoryDtos = new();
            using (var command = this._MasterContext.Database.GetDbConnection().CreateCommand())
            {
                string strSQL = "SP_GetCategoryByOrgId";
                command.CommandText = strSQL;
                command.CommandType = CommandType.StoredProcedure;
                var parameter = command.CreateParameter();
                command.Parameters.Add(new SqlParameter("@orgid", OrgId));

                await this._MasterContext.Database.OpenConnectionAsync();
                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        CategoryDTO categoryDTO = new CategoryDTO();

                        categoryDTO.CategoryId = Convert.ToInt32(result["CategoryId"]);
                        categoryDTO.CategoryName = result["CategoryName"].ToString();
                        categoryDTO.Urlpath = result["Urlpath"].ToString();
                        categoryDTO.ParentCategoryId = Convert.ToInt32(result["ParentCategoryId"]);
                        categoryDTO.Description = result["Description"].ToString();
                        categoryDTO.Icon = result["Icon"].ToString();
                        categoryDTO.Banner = result["Banner"].ToString();
                        categoryDTO.IsPublished = (bool)result["IsPublished"];
                        categoryDTO.IsShowHomePage = (bool)result["IsShowHomePage"];
                        categoryDTO.IsIncludeMenu = (bool)result["IsIncludeMenu"];
                        categoryDTO.SeoPageName = result["SeoPageName"].ToString();
                        categoryDTO.SeoTitle = result["SeoTitle"].ToString();
                        categoryDTO.SeoKeyword = result["SeoKeyword"].ToString();
                        categoryDTO.SeoDescription = result["SeoDescription"].ToString();
                        categoryDTO.InsertDate = (DateTime)result["InsertDate"];
                        categoryDTO.ModifiedDate = (DateTime)result["ModifiedDate"];
                        categoryDTO.UserName = result["UserName"].ToString();
                        categoryDTO.DisplayOrder = Convert.ToInt32(result["DisplayOrder"]);
                        categoryDTO.OrgId = Convert.ToInt32(OrgId);

                        categoryDtos.Add(categoryDTO);
                    }
                }
            }
            return categoryDtos;
        }
    }
}
