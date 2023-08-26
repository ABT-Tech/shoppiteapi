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
    public class OrganizationRepository : IOrganizationRepository
    {
        protected readonly Shoppite_masterContext _MasterContext;
        public OrganizationRepository(Shoppite_masterContext dbContext)
        {
            _MasterContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<List<OrganizationDTO>> GetOrganizations(int? Org_CategoryId) 
        {
            List<OrganizationDTO> organizationDTOs = new List<OrganizationDTO>();
            using (var command = this._MasterContext.Database.GetDbConnection().CreateCommand())
            {
                string strSQL = "SP_GetAllOrganization";

                command.CommandText = strSQL;
                command.CommandType = CommandType.StoredProcedure;
                var parameter = command.CreateParameter();
                parameter.ParameterName = "@Org_CategoryId";
                parameter.Value = Org_CategoryId;
                command.Parameters.Add(parameter);
                await this._MasterContext.Database.OpenConnectionAsync();

                using (var result = await command.ExecuteReaderAsync())
                {
                    while (result.Read())
                    {
                        OrganizationDTO organizationDTO = new OrganizationDTO();
                        organizationDTO.OrgId = Convert.ToInt32(result["OrgId"]);
                        organizationDTO.ShopName = result["ShopName"].ToString();
                        organizationDTO.Image = result["Image"].ToString();
                        organizationDTO.VenderName = result["VenderName"].ToString();
                        organizationDTO.VenderEmail = result["VendorEmail"].ToString();
                        organizationDTO.VenderMobile = result["VendorMobile"].ToString();
                        organizationDTO.OrgDescription = result["OrgDescription"].ToString();
                        organizationDTO.IsPublished = result["IsPublished"] != DBNull.Value ? (bool)result["IsPublished"] : false;
                        organizationDTO.IsActive = result["IsActive"] != DBNull.Value ? (bool)result["IsActive"] : false;
                        organizationDTOs.Add(organizationDTO);
                    }
                }
            }
            return organizationDTOs;

        }
        public async Task<List<OrganizationCategoryDTO>> GetOrganizationCategories()
        {
            List<OrganizationCategoryDTO> organizationcategory= new List<OrganizationCategoryDTO>();
            using (var command = this._MasterContext.Database.GetDbConnection().CreateCommand())
            {
                string strSQL = "SELECT OrganizationCategory.Org_CategoryId, " +
                                "OrganizationCategory.CategoryName, " +
                                "OrganizationCategory.CategoryImage " +
                                "FROM OrganizationCategory " +
                                "WhERE OrganizationCategory.IsActive=1 Order by SortOrder asc "
                                ;

                command.CommandText = strSQL;
                command.CommandType = CommandType.Text;

                await this._MasterContext.Database.OpenConnectionAsync();

                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        OrganizationCategoryDTO organizationDTO = new OrganizationCategoryDTO();
                        organizationDTO.CategoryName =result["CategoryName"].ToString();
                        organizationDTO.CategoryImage = result["CategoryImage"].ToString();
                        organizationDTO.Org_CategoryId = Convert.ToInt32(result["Org_CategoryId"]);
                        organizationcategory.Add(organizationDTO);
                    }
                }
            }
            return organizationcategory;

        }
    }
}
