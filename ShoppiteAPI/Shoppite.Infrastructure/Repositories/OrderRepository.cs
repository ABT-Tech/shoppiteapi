using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Shoppite.Core.DTOs;
using Shoppite.Core.Model;
using Shoppite.Core.Repositories;
using Shoppite.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        protected readonly Shoppite_masterContext _MasterContext;
        public OrderRepository(Shoppite_masterContext dbContext)
        {
            _MasterContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task BuyNow(OrdersDTO orders)
        {
            try
            {
                OrderBasic ob = new OrderBasic();
                {
                    decimal? orderTotal=0;
                    var check = await _MasterContext.OrderBasics.Where(x => x.OrderGuid == orders.OrderGuid && x.OrderStatus == "Cart").ToListAsync();
                   
                    for (int i = 0; i < check.Count; i++)                       
                    {
                        for (int j = 0; j < orders.ProductLists.Count; j++)
                        {
                            if (check[i].OrderId == orders.ProductLists[j].OrderId)
                            {
                                check[i].OrderStatus = "Confirmed";
                                check[i].Qty = orders.ProductLists[j].Qty;
                                _MasterContext.OrderBasics.Update(check[i]);
                                await _MasterContext.SaveChangesAsync();
                                orderTotal += check[i].Price * check[i].Qty;
                                orders.BaseTotalPrice = orderTotal;                                
                            }
                        }
                    }
                }
                OrderShipping shipping = new();
                {
                    var OrderCheck = _MasterContext.OrderShippings.FirstOrDefault(x => x.OrderGuid == orders.OrderGuid);
                    var getUsername = _MasterContext.OrderBasics.FirstOrDefault(x => x.OrderGuid == orders.OrderGuid);
                    if (OrderCheck == null)
                    {
                        shipping.OrderGuid = orders.OrderGuid;
                        shipping.UserName = getUsername.UserName;
                        shipping.Contactnumber = orders.Contactnumber;
                        shipping.Phone = orders.Contactnumber;
                        shipping.Email = getUsername.UserName;
                        shipping.Address = orders.Address.AddressTitle;
                        shipping.City = orders.Address.SelectCity;
                        shipping.Street = orders.Address.SelectStreet;
                        shipping.OrgId = getUsername.OrgId;
                        shipping.InsertDate = DateTime.Now;
                        _MasterContext.OrderShippings.Add(shipping);
                        await _MasterContext.SaveChangesAsync();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<MyOrdersDTO>> GetMyOrderDetails(int OrgId, int UserId)
        {
            List<MyOrdersDTO> OrdersDTO = new List<MyOrdersDTO>();
            using (var command = this._MasterContext.Database.GetDbConnection().CreateCommand())
            {
                string strSQL = "SP_GetMyOrderDetails";
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
                        MyOrdersDTO myOrderDTO = new MyOrdersDTO();
                        var ProductStrList = result["ProductList"].ToString();
                        var ProductList = ProductStrList.Split(',');
                        myOrderDTO.Id = Convert.ToInt32(result["Id"]);
                        myOrderDTO.Title = result["Title"].ToString();
                        myOrderDTO.Image = result["Image"].ToString();
                        myOrderDTO.Brand = result["Brand"].ToString();
                        myOrderDTO.Quantity = Convert.ToInt32(result["Quantity"]);
                        myOrderDTO.Price = Convert.ToDouble(result["Price"]);
                        myOrderDTO.OldPrice = Convert.ToInt32(result["OldPrice"]);
                        myOrderDTO.orgId = Convert.ToInt32(OrgId);
                        myOrderDTO.OrderDate = (DateTime)result["OrderDate"];
                        myOrderDTO.OrderStatus = result["OrderStatus"].ToString();
                        myOrderDTO.orderGuId = (Guid)result["orderGuId"];
                        myOrderDTO.UserId = Convert.ToInt32(UserId);
                        myOrderDTO.ProductList = ProductList;
                        OrdersDTO.Add(myOrderDTO);
                    }
                }
            }
            return OrdersDTO;
        }
    }
}
