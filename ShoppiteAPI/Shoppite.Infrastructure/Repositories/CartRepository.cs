using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Shoppite.Core.DTOs;
using Shoppite.Core.Model;
using Shoppite.Core.Repositories;
using Shoppite.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Shoppite.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        protected readonly Shoppite_masterContext _MasterContext;
        public CartRepository(Shoppite_masterContext dbContext)
        {
            _MasterContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
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
        public async Task AddtoFavourite(Favourite favourite)
        {
            var username = await _MasterContext.Users.FirstOrDefaultAsync(x => x.UserId==favourite.UserId);
            try
            {          
                CustomerWishlist cw = new();
                {
                    cw.ProductId = favourite.proId;
                    cw.InsertDate = DateTime.Now;
                    cw.UserName = username.Username;
                    cw.Ip = null;
                    cw.OrgId = favourite.orgId;
                    _MasterContext.CustomerWishlists.Add(cw);
                    await _MasterContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<ChangeAddress>> GetAddressByUserId(int OrgId, int UserId)
        {
            List<ChangeAddress> changeAddress = new List<ChangeAddress>();
            using (var command = this._MasterContext.Database.GetDbConnection().CreateCommand())
            {
                string strSQL = "SP_GetAddressBYUserId";
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
                        ChangeAddress address = new ChangeAddress();
                        address.SelectCity = result["SelectCity"].ToString();
                        address.SelectCountry = result["SelectCountry"].ToString();
                        address.AddressDetail = result["AddressDetail"].ToString();
                        address.orgId = Convert.ToInt32(OrgId);
                        address.UserId = Convert.ToInt32(UserId);
                        changeAddress.Add(address);
                    }
                }
            }
            return changeAddress;

        }

    }
}
