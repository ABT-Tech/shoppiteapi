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
        public async Task<List<OrganizationDTO>> GetOrganizations() 
        {
            List<OrganizationDTO> organizationDTOs = new List<OrganizationDTO>();
            using (var command = this._MasterContext.Database.GetDbConnection().CreateCommand())
            {
                string strSQL = "SELECT organization.id AS OrgId, " +
                                "organization.org_name AS ShopName, " +
                                "organization.vender_name AS VenderName," +
                                "organization.v_email AS VendorEmail," +
                                "organization.v_mobile AS VendorMobile," +
                                "organization.org_description AS OrgDescription," +
                                "Logo.Logo AS IMAGE " +
                                "FROM organization " +
                                "JOIN logo ON organization.id = logo.orgid ";

                command.CommandText = strSQL;
                command.CommandType = CommandType.Text;

                await this._MasterContext.Database.OpenConnectionAsync();

                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        OrganizationDTO organizationDTO = new OrganizationDTO();
                        organizationDTO.OrgId =  Convert.ToInt32(result["OrgId"]);
                        organizationDTO.ShopName = result["ShopName"].ToString();
                        organizationDTO.Image = result["Image"].ToString();
                        organizationDTO.VenderName = result["VenderName"].ToString();
                        organizationDTO.VenderEmail = result["VendorEmail"].ToString();
                        organizationDTO.VenderMobile = result["VenderMobile"].ToString();
                        organizationDTO.OrgDescription = result["OrgDescription"].ToString();
                        organizationDTOs.Add(organizationDTO);
                    }
                }
            }
            return organizationDTOs;

        }
    }
}
