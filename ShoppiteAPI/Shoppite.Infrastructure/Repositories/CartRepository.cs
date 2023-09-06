using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Shoppite.Core.DTOs;
using Shoppite.Core.Model;
using Shoppite.Core.Repositories;
using Shoppite.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

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
                parameter = command.CreateParameter();
                parameter.ParameterName = "@SpecificationId";
                parameter.Value = Cart.SpecificationId;
                command.Parameters.Add(parameter);
                await this._MasterContext.Database.OpenConnectionAsync();

                var finduser = _MasterContext.Users.FirstOrDefault(u => u.UserId == Cart.UserId&& u.OrgId==Cart.orgId);
                var username = finduser.Email;
                var productsdetail = _MasterContext.ProductBasics.FirstOrDefault(p => p.ProductId == Cart.proId && p.OrgId == Cart.orgId);
                var cartdetails = _MasterContext.OrderBasics.FirstOrDefault(u => u.ProductId == Cart.proId && u.OrgId == Cart.orgId && u.UserName == username&&u.OrderStatus=="Cart");
                var getSpecId = _MasterContext.ProductSpecifications.FirstOrDefault(p => p.SpecificationId == Cart.SpecificationId&& p.ProductGuid== productsdetail.ProductGuid && p.OrgId == Cart.orgId);
                
                if(cartdetails != null & Cart.SpecificationId != 0)
                {
                    var getProductSpecId = _MasterContext.OrderVariations.FirstOrDefault(ps => ps.ProductSpecificationId == getSpecId.ProductSpecificationId && ps.OrderGuid == cartdetails.OrderGuid);
                    if (getProductSpecId!=null)
                    {
                        if (cartdetails.ProductId == Cart.proId&&getSpecId.ProductSpecificationId==getProductSpecId.ProductSpecificationId)
                        {
                            cartdetails.InsertDate = DateTime.Now;
                            cartdetails.Qty = cartdetails.Qty + Cart.Qty;
                            _MasterContext.OrderBasics.Update(cartdetails);
                            await _MasterContext.SaveChangesAsync();
                        }
                    }
                    else
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                }
                else if(Cart.SpecificationId==0)
                {
                    if (cartdetails != null && cartdetails.ProductId == Cart.proId)
                    {
                        cartdetails.InsertDate = DateTime.Now;
                        cartdetails.Qty = cartdetails.Qty + Cart.Qty;
                        _MasterContext.OrderBasics.Update(cartdetails);
                        await _MasterContext.SaveChangesAsync();
                    }
                    else
                    {
                        await command.ExecuteNonQueryAsync();
                    }

                }
                
                else
                {
                    await command.ExecuteNonQueryAsync();
                }
            }
            return "success";
        }
        public async Task<List<CartDTO>> GetCartDetails(int OrgId, int UserId)
        {
            List<CartDTO> cartDTOs = new();
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
                        cartDTO.SpecificationNames = result["SpecificationNames"].ToString();
                        cartDTO.SpecificationId = Convert.ToInt32(result["SpecificationIds"]);
                        cartDTO.Quantity = Convert.ToInt32(result["Quantity"]);
                        cartDTO.ProductQty = Convert.ToInt32(result["qty"]);
                        cartDTO.Price = Convert.ToDouble(result["Price"]);
                        cartDTO.OldPrice = result["OldPrice"] != DBNull.Value ? Convert.ToInt32(result["OldPrice"]):0;
                        cartDTO.orgId = Convert.ToInt32(OrgId);
                        cartDTO.UserId = Convert.ToInt32(UserId);
                        cartDTO.orderid = Convert.ToInt32(result["orderid"]);
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
            var username = await _MasterContext.Users.FirstOrDefaultAsync(x => x.UserId==favourite.UserId&&x.OrgId==favourite.orgId);
            var ProductId = await _MasterContext.ProductBasics.FirstOrDefaultAsync(x => x.ProductId == favourite.proId && x.OrgId == favourite.orgId);
            var ProductSpecificationId = await _MasterContext.ProductSpecifications.FirstOrDefaultAsync(p => p.ProductGuid == ProductId.ProductGuid && p.OrgId == favourite.orgId&&p.SpecificationId==favourite.SpecificationId);
            try
            {          
                CustomerWishlist cw = new();
                {
                    cw.ProductId = favourite.proId;
                    cw.InsertDate = DateTime.Now;
                    cw.UserName = username.Email;
                    cw.Ip = null;
                    cw.OrgId = favourite.orgId;       
                    if(favourite.SpecificationId!=0)
                    {
                        cw.ProductSpecificationId = ProductSpecificationId.ProductSpecificationId;
                    }
                    else
                    {
                        cw.ProductSpecificationId = 0;
                    }
                 
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
            List<ChangeAddress> changeAddress = new();
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
                        ChangeAddress address = new();
                        address.SelectCity = result["SelectCity"].ToString();
                        address.zipcode = result["zipcode"].ToString();
                        address.SelectState = result["SelectState"].ToString();
                        address.AddressDetail = result["AddressDetail"].ToString();
                        address.orgId = Convert.ToInt32(OrgId);
                        address.UserId = Convert.ToInt32(UserId);
                        address.ContactNumber = result["Contactnumber"].ToString();
                        changeAddress.Add(address);
                    }
                }
            }
            return changeAddress;
        }
        public async Task RemovefromFavourite(int ProductId, int UserId, int OrgId,int? SpecificationId)
        {
            var username = _MasterContext.Users.FirstOrDefault(u => u.UserId == UserId);
            CustomerWishlist cuswishlist = _MasterContext.CustomerWishlists.FirstOrDefault(u => u.ProductId == ProductId && u.UserName == username.Email &&u.OrgId== OrgId);
        
            if (cuswishlist != null&&SpecificationId==0||SpecificationId==null)
            {
                _MasterContext.CustomerWishlists.Remove(cuswishlist);
                await _MasterContext.SaveChangesAsync();
            }
            else
            {
                var getspecId = _MasterContext.SpecificationSetups.FirstOrDefault(s => s.SpecificationId == SpecificationId && s.OrgId == OrgId);
                var productId = _MasterContext.ProductBasics.FirstOrDefault(p => p.ProductId == ProductId && p.OrgId == OrgId);
                var productSpecId = _MasterContext.ProductSpecifications.FirstOrDefault(ps => ps.SpecificationId == getspecId.SpecificationId && ps.OrgId == OrgId&&ps.ProductGuid==productId.ProductGuid);
                CustomerWishlist wishlist = _MasterContext.CustomerWishlists.FirstOrDefault(u => u.ProductId == ProductId && u.UserName == username.Email && u.OrgId == OrgId&&u.ProductSpecificationId== productSpecId.ProductSpecificationId);
                if (wishlist != null)
                {
                    _MasterContext.CustomerWishlists.Remove(wishlist);
                    await _MasterContext.SaveChangesAsync();
                }
            }
        }
        public async Task RemoveFromCart(int userId, int proId, int orgId,int? SpecificationId)
        {
            var username = _MasterContext.Users.FirstOrDefault(u => u.UserId == userId);
            var cart = _MasterContext.OrderBasics.FirstOrDefault(u => u.ProductId == proId && u.UserName == username.Email && u.OrgId == orgId&&u.OrderStatus=="Cart");
            OrderMaster details = await _MasterContext.OrderMasters.FirstOrDefaultAsync(a => a.OrderGuid == cart.OrderGuid && a.OrgId == orgId);
            if (SpecificationId == 0 || SpecificationId == null)
            {
                if (cart != null)
                {
                    _MasterContext.OrderBasics.Remove(cart);
                    await _MasterContext.SaveChangesAsync();
                }
                var cartdetails = _MasterContext.OrderBasics.FirstOrDefault(u => u.UserName == username.Email && u.OrderStatus == "Cart" && u.OrgId == orgId);
                if (cartdetails == null)
                {
                    _MasterContext.OrderMasters.Remove(details);
                    await _MasterContext.SaveChangesAsync();
                }
            }
            else
            {
                var productId = _MasterContext.ProductBasics.FirstOrDefault(x => x.ProductId == proId && x.OrgId == orgId);
                var productsepcId = _MasterContext.ProductSpecifications.FirstOrDefault(x => x.ProductGuid == productId.ProductGuid && x.OrgId == orgId && x.SpecificationId == SpecificationId & x.OrgId == orgId);
                var ordervariation = _MasterContext.OrderVariations.FirstOrDefault(ov => ov.OrderGuid == cart.OrderGuid && ov.OrgId == orgId && ov.ProductSpecificationId == productsepcId.ProductSpecificationId & ov.OrgId == orgId);
                var q = (from variation in _MasterContext.OrderVariations
                         join order in _MasterContext.OrderBasics on variation.OrderId equals order.OrderId
                         join ordermaster in _MasterContext.OrderMasters on order.OrderGuid equals ordermaster.OrderGuid
                         where variation.ProductSpecificationId == productsepcId.ProductSpecificationId && order.OrderStatus == "Cart" && order.UserName == username.Email && order.OrgId == orgId
                         select new
                         {
                             ov = variation,
                             Ob = order,
                             om = ordermaster
                         }).FirstOrDefault();

                _MasterContext.OrderVariations.Remove(q.ov);
                _MasterContext.OrderBasics.Remove(q.Ob);
                var cartdetails = _MasterContext.OrderBasics.FirstOrDefault(u => u.UserName == username.Email && u.OrderStatus == "Cart" && u.OrgId == orgId);
                if (cartdetails == null)
                {
                    _MasterContext.OrderMasters.Remove(q.om);
                }                
                await _MasterContext.SaveChangesAsync();
            }            
         }
        public async Task<CartDTO> GetNoOfItemsInCart(int OrgId, int UserId)
        {
            CartDTO cartDTOs = new();
            using (var command = this._MasterContext.Database.GetDbConnection().CreateCommand())
            {
                string strSQL = "SP_GetNumOfItems_InCart";
                command.CommandText = strSQL;
                command.CommandType = CommandType.StoredProcedure;
                var parameter = command.CreateParameter();
                command.Parameters.Add(new SqlParameter("@OrgId", OrgId));
                command.Parameters.Add(new SqlParameter("@UserId", UserId));
                await this._MasterContext.Database.OpenConnectionAsync();
                using var result = await command.ExecuteReaderAsync();
                while (await result.ReadAsync())
                {
                    cartDTOs.orgId = Convert.ToInt32(OrgId);
                    cartDTOs.UserId = Convert.ToInt32(UserId);
                    cartDTOs.NumOfItems = Convert.ToInt32(result["NumOfProduct"]);
                }
            }
            return cartDTOs;
        }
    }
}
