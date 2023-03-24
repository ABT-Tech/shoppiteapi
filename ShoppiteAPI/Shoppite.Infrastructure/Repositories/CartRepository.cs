using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Shoppite.Core.DTOs;
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
    public class CartRepository : ICartRepository
    {
        protected readonly Shoppite_masterContext _MasterContext;
        public CartRepository(Shoppite_masterContext dbContext)
        {
            _MasterContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
     
        public async Task<string> AddToCart(CartRequest Cart)
        {
            List<CartDTO> cartDTOs = new List<CartDTO>();
            using (var command = this._MasterContext.Database.GetDbConnection().CreateCommand())
            {
                string strSQL = "SP_AddToCart";

                command.CommandText = strSQL;
                command.CommandType = CommandType.StoredProcedure;
                var parameter = command.CreateParameter();
                parameter.ParameterName = "@OrgId";
                parameter.Value = Cart.orgId;
                command.Parameters.Add(parameter);
                parameter = command.CreateParameter();
                parameter.ParameterName = "@UserId";
                parameter.Value = Cart.UserId;
                command.Parameters.Add(parameter);
                parameter = command.CreateParameter();
                parameter.ParameterName = "@ProId";
                parameter.Value = Cart.proId;
                command.Parameters.Add(parameter);
                parameter = command.CreateParameter();
                parameter.ParameterName = "@Qty";
                parameter.Value = Cart.Qty;
                command.Parameters.Add(parameter);
                await this._MasterContext.Database.OpenConnectionAsync();

                await command.ExecuteNonQueryAsync();
                
            }
            return "success";
        }
        public async Task<List<CartDTO>> GetCartDetails(int OrgId, int UserId)
        {
            List<CartDTO> cartDTOs = new List<CartDTO>();
            using (var command = this._MasterContext.Database.GetDbConnection().CreateCommand())
            {
                string strSQL = "SP_GetCartDetails";
                command.CommandText = strSQL;
                command.CommandType = CommandType.StoredProcedure;
                var parameter = command.CreateParameter();
                command.Parameters.Add(new SqlParameter("@OrgId", OrgId));
                command.Parameters.Add(new SqlParameter("@UserId", UserId));
                await this._MasterContext.Database.OpenConnectionAsync();
                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        CartDTO cartDTO = new CartDTO();
                        var ProductStrList = result["ProductList"].ToString();
                        var ProductList = ProductStrList.Split(',');
                        cartDTO.Id = Convert.ToInt32(result["Id"]);
                        cartDTO.Title = result["Title"].ToString();
                        cartDTO.Image = result["Image"].ToString();
                        cartDTO.Brand = result["Brand"].ToString();
                        cartDTO.Quantity = Convert.ToInt32(result["Quantity"]);
                        cartDTO.Price = Convert.ToDouble(result["Price"]);
                        cartDTO.OldPrice = Convert.ToInt32(result["OldPrice"]);
                        cartDTO.orgId = Convert.ToInt32(OrgId);
                        cartDTO.UserId = Convert.ToInt32(UserId);
                        cartDTO.orderGuId = (Guid)result["orderGuId"];
                        cartDTO.ProductList = ProductList;
                        cartDTOs.Add(cartDTO);
                    }
                }
            }
            return cartDTOs;
        }
    }
}
