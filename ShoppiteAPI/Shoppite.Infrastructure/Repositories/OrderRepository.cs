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
            int? productId = 0;
            int? producQty = 0;
            try
            {
                OrderBasic ob = new();
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
                                check[i].InsertDate = DateTime.Now;
                                _MasterContext.OrderBasics.Update(check[i]);
                                await _MasterContext.SaveChangesAsync();
                                orderTotal += check[i].Price * check[i].Qty;
                                orders.BaseTotalPrice = orderTotal;
                                productId = orders.ProductLists[j].Id;
                                producQty = orders.ProductLists[j].Qty;
                            }
                        }
                    }
                }
                var findQty = _MasterContext.ProductBasics.FirstOrDefault(x => x.ProductId == productId);
                if(findQty!=null)
                {
                    findQty.Qty = findQty.Qty - producQty;
                    _MasterContext.ProductBasics.Update(findQty);
                    await _MasterContext.SaveChangesAsync();
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
                        shipping.Address = orders.Address.AddressDetail;
                        shipping.Zipcode = orders.Address.zipcode;
                        shipping.City = orders.Address.SelectCity;
                        shipping.Street = orders.Address.SelectState;
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
            List<MyOrdersDTO> OrdersDTO = new();
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
                        myOrderDTO.orderid = Convert.ToInt32(result["orderid"]);
                        myOrderDTO.ProductList = ProductList;
                        OrdersDTO.Add(myOrderDTO);
                    }
                }
            }
            return OrdersDTO;
        }
        public async Task<OrderDetails> GetOrderDetailsByOrgId(int OrgId,int OrderMasterId)
        {
            OrderDetails orderDetails = new();
            orderDetails.ProductLists = new();
            using (var command = this._MasterContext.Database.GetDbConnection().CreateCommand())
            {
                string strSQL = "SP_GetOrderDetails_Vendor_ByOrgId";
                command.CommandText = strSQL;
                command.CommandType = CommandType.StoredProcedure;
                var parameter = command.CreateParameter();
                command.Parameters.Add(new SqlParameter("@OrgId", OrgId));
                command.Parameters.Add(new SqlParameter("@OrderMasterId", OrderMasterId));
                await this._MasterContext.Database.OpenConnectionAsync();
                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {                           
                     OrderListModel orderListModel = new OrderListModel();
                     orderListModel.orgId = Convert.ToInt32(result["orgId"]);
                     orderListModel.OrderStatus= result["OrderStatus"].ToString();
                     orderListModel.UserId= Convert.ToInt32(result["UserId"]);
                     orderListModel.Id = Convert.ToInt32(result["Id"]);
                     orderListModel.Title = result["Title"].ToString();
                     orderListModel.Image = result["Image"].ToString();
                     orderListModel.Brand = result["Brand"].ToString();
                     orderListModel.Quantity = Convert.ToInt32(result["Quantity"]);
                     orderListModel.Price = Convert.ToDouble(result["Price"]);
                     orderDetails.ProductLists.Add(orderListModel);               

                    }
                }
            }
            using (var command = this._MasterContext.Database.GetDbConnection().CreateCommand())
            {
                string strSQL = "SELECT Users.UserId,Users.OrgId, CONVERT(DATE, Order_Basic.InsertDate) AS OrderDate, "+
                               "SUM(Order_Basic.Price*Order_Basic.QTY) As TotalPrice, " +
                              "CONCAT(shiiping.Address + ''+',', shiiping.City + ''+',',shiiping.Street+ ''+',', shiiping.Zipcode) AS Address " +
                              "FROM Order_Basic " +
                              "Inner JOIN Users ON Order_Basic.UserName = Users.UserName " +
                              "Inner JOIN Order_Shipping AS shiiping ON Order_Basic.OrderGuid = shiiping.OrderGuid " +
                              "inner join Order_Master om on  Order_Basic.OrderGUID=om.OrderGUID " +
                              "WHERE Order_Basic.OrgId= " + OrgId + " And om.OrderMasterId= " + OrderMasterId+
                              "Group By Users.UserId,Users.OrgId, CONVERT(DATE, Order_Basic.InsertDate), " +
                              "CONCAT(shiiping.Address + ''+',', shiiping.City + ''+',',shiiping.Street+ ''+',', shiiping.Zipcode) ";
                             
                command.CommandText = strSQL;
                command.CommandType = CommandType.Text;
                var parameter = command.CreateParameter();
                await this._MasterContext.Database.OpenConnectionAsync();               
                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        orderDetails.Address= result["Address"].ToString();
                        orderDetails.Date = Convert.ToDateTime(result["OrderDate"]).ToString("dd/MM/yyyy");
                        orderDetails.TotalPrice = Convert.ToDouble(result["TotalPrice"]);
                        orderDetails.orgId = Convert.ToInt32(result["OrgId"]);
                        orderDetails.userId = Convert.ToInt32(result["UserId"]);
                    }
                }
            }         
            return orderDetails;
        }
        public async Task<List<VendorsOrder>> GetOrdersDetailForVendor(int OrgId)
        {
            List<VendorsOrder> Orders = new();
            using (var command = this._MasterContext.Database.GetDbConnection().CreateCommand())
            {
                string strSQL = "SP_GetOrderDetails_Vendor";
                command.CommandText = strSQL;
                command.CommandType = CommandType.StoredProcedure;
                var parameter = command.CreateParameter();
                command.Parameters.Add(new SqlParameter("@orgid", OrgId));
                await this._MasterContext.Database.OpenConnectionAsync();
                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        VendorsOrder orderDetails = new VendorsOrder();
                        orderDetails.orgId = Convert.ToInt32(OrgId);
                        orderDetails.userId = Convert.ToInt32(result["userId"]);
                        orderDetails.orderId = Convert.ToInt32(result["OrderMasterId"]);
                        orderDetails.Price = Convert.ToDouble(result["Price"]);
                        Orders.Add(orderDetails);
                    }
                }
            }
            return Orders;
        }
        public async Task UpdateOrderStatus(Orders orders)
        {
            var orderStatus = _MasterContext.OrderStatuses.FirstOrDefault(a => a.OrgId ==orders.orgId && a.OrderId==orders.OrderId);
            if(orderStatus!=null)
            {
                orderStatus.Remarks = orders.Remark;
                orderStatus.OrderStatus1 = orders.orderstatus;

                _MasterContext.Entry(orderStatus).State = EntityState.Detached;
                _MasterContext.Entry(orderStatus).State = EntityState.Modified;
                await _MasterContext.SaveChangesAsync();
            }
        }
    }
}
