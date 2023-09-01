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
using Microsoft.Extensions.Configuration;

namespace Shoppite.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        protected readonly Shoppite_masterContext _MasterContext;
        protected readonly IConfiguration _configuration;
        public CategoryRepository(Shoppite_masterContext dbContext, IConfiguration configuration)
        {
            _MasterContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        
        public async Task<List<CategoryDTO>> GetAllCategory(int OrgId) 
        {
            var IsCouponEnabled = _configuration.GetSection("CouponSettings")["IsCoupanEnabled"];
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
                      /*  var BannerStrList = result["BannerList"].ToString();
                        var Banner = BannerStrList.Split(',');*/
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
                        if (IsCouponEnabled == "1")
                        {
                            categoryDTO.IsCouponEnabled = true;
                        }
                        else
                        {
                            categoryDTO.IsCouponEnabled = false;
                        }
                        categoryDtos.Add(categoryDTO);
                    }
                }
            }
            return categoryDtos;
        }
        public async Task<List<MainCategoryDTO>> GetAllParentcategories()
        {
          
            List<MainCategoryDTO> categoryDtos = new();
            using (var command = this._MasterContext.Database.GetDbConnection().CreateCommand())
            {
                string strSQL = "SELECT Category_Master.Category_Id AS MainCategoryId, " +
                               "Category_Master.Category_Name AS MainCategory, " +
                               "Category_Master.Icon AS MainCategoryImage " +
                               "FROM Category_Master " +
                               "WhERE  Category_Master.Parent_Category_Id=0 AND Category_Master.IspUBLISHED=1 Order by Category_Name asc "
                               ;

                command.CommandText = strSQL;
                command.CommandType = CommandType.Text;


                await this._MasterContext.Database.OpenConnectionAsync();
                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        MainCategoryDTO categoryDTO = new MainCategoryDTO();
                        categoryDTO.MainCategoryId= Convert.ToInt32(result["MainCategoryId"]);
                        categoryDTO.MainCategory= result["MainCategory"].ToString();
                        categoryDTO.MainCategoryImage= result["MainCategoryImage"].ToString();
                        categoryDtos.Add(categoryDTO);
                    }
                }
            }
            return categoryDtos;
        }
        public async Task<List<CategoriesDTO>> GetAllCategoriesByMainCategory(int MainCategoryId)
        {
            
            List<CategoriesDTO> categoryDtos = new();
            using (var command = this._MasterContext.Database.GetDbConnection().CreateCommand())
            {
                string strSQL = "SP_GetAllCategories";
                command.CommandText = strSQL;
                command.CommandType = CommandType.StoredProcedure;
                var parameter = command.CreateParameter();
                command.Parameters.Add(new SqlParameter("@MainCategoryId", MainCategoryId));

                await this._MasterContext.Database.OpenConnectionAsync();
                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        CategoriesDTO categoryDTO = new CategoriesDTO();
                        categoryDTO.MainCategoryId= Convert.ToInt32(result["MainCategoryId"]);
                        categoryDTO.CategoryId= Convert.ToInt32(result["CategoryId"]);
                        categoryDTO.CategoryName=result["CategoryName"].ToString();
                        categoryDTO.CategoryImage= result["CategoryImage"].ToString();

                        categoryDtos.Add(categoryDTO);
                    }
                }
            }
            return categoryDtos;
        }
    }
}
