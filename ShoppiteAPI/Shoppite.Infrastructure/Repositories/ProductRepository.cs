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
    public class ProductRepository : IProductRepository
    {
        protected readonly Shoppite_masterContext _MasterContext;
        public ProductRepository(Shoppite_masterContext dbContext)
        {
            _MasterContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<List<ProductsDTO>> GetAllProductsByOrganizations(int orgId)
        {
            List<ProductsDTO> productsDTOs = new List<ProductsDTO>();
            using (var command = this._MasterContext.Database.GetDbConnection().CreateCommand())
            {
                string strSQL = "GetAllProductsByOrganizations";

                command.CommandText = strSQL;
                command.CommandType = CommandType.StoredProcedure;
                var parameter = command.CreateParameter();
                parameter.ParameterName = "@Org_ID";
                parameter.Value = orgId;
                command.Parameters.Add(parameter);
                await this._MasterContext.Database.OpenConnectionAsync();

                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        ProductsDTO productsDTO = new ProductsDTO();
                        var ProductStrList = result["ProductList"].ToString();
                        var ProductList = ProductStrList.Split(',');
                        productsDTO.Id = Convert.ToInt32(result["Id"]);
                        productsDTO.Title = result["Title"].ToString();
                        productsDTO.Image = result["Image"].ToString();
                        productsDTO.Brand = result["Brand"].ToString();
                        productsDTO.Price = Convert.ToDouble(result["Price"]);
                        productsDTO.OldPrice = Convert.ToDouble(result["OldPrice"]);
                        productsDTO.ProductList = ProductList;
                        productsDTO.orgId = Convert.ToInt32(orgId);
                        productsDTOs.Add(productsDTO);
                    }
                }
            }
            return productsDTOs;

        }
        public async Task<List<ProductsDTO>> GetWishlistByUser(int orgId, int userId) 
        {
            List<ProductsDTO> productsDTOs = new List<ProductsDTO>();
            using (var command = this._MasterContext.Database.GetDbConnection().CreateCommand())
            {
                string strSQL = "SP_GetWishlistByUser";

                command.CommandText = strSQL;
                command.CommandType = CommandType.StoredProcedure;
                var parameter = command.CreateParameter();
                command.Parameters.Add(new SqlParameter("@orgid", orgId));
                command.Parameters.Add(new SqlParameter("@userid", userId));
                // command.Parameters.Add(parameter);
                await this._MasterContext.Database.OpenConnectionAsync();

                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        ProductsDTO productsDTO = new ProductsDTO();
                        var ProductStrList = result["ProductList"].ToString();
                        var ProductList = ProductStrList.Split(',');
                        productsDTO.Id = Convert.ToInt32(result["Id"]);
                        productsDTO.Title = result["Title"].ToString();
                        productsDTO.Image = result["Image"].ToString();
                        productsDTO.Brand = result["Brand"].ToString();
                        productsDTO.Price = Convert.ToDouble(result["Price"]);
                        productsDTO.OldPrice = Convert.ToDouble(result["OldPrice"]);
                        productsDTO.ProductList = ProductList;
                        productsDTO.orgId = Convert.ToInt32(orgId);
                        productsDTOs.Add(productsDTO);
                    }
                }
            }
            return productsDTOs;

        }
    }
}
