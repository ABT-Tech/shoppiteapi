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
            var getUsername = await _MasterContext.Users.FirstOrDefaultAsync(u => u.UserId == orders.UserId && u.OrgId == orders.orgid);
            if (orders.OrderGuid == Guid.Empty)
            {
                OrderMaster orderMaster = new();
                orderMaster.InsertDate = DateTime.Now;
                orderMaster.OrderKeyStatus = "Active";
                orderMaster.OrgId = orders.orgid;

                _MasterContext.OrderMasters.Add(orderMaster);
                await _MasterContext.SaveChangesAsync();

                int p = 0;
                var productDetail = _MasterContext.ProductBasics.FirstOrDefault(x => x.ProductId == orders.ProductLists[p].Id);
                var Price = _MasterContext.ProductPrices.FirstOrDefault(x => x.ProductGuid == productDetail.ProductGuid);              
                OrderBasic buynow = new();
                buynow.OrderGuid = orderMaster.OrderGuid;
                buynow.ProductId = orders.ProductLists[p].Id;
                buynow.Qty = orders.ProductLists[p].Quantity;
                buynow.Price = Price.Price;
                buynow.UserName = getUsername.Email;
                buynow.InsertDate = DateTime.Now;
                buynow.OrderStatus = "Confirmed";
                buynow.PaymentMode = "COD";
                buynow.OrgId = orders.orgid;

                _MasterContext.OrderBasics.Add(buynow);
                await _MasterContext.SaveChangesAsync();

                var findQty = _MasterContext.ProductBasics.FirstOrDefault(x => x.ProductId == buynow.ProductId);
                if(findQty!=null)
                {
                    findQty.Qty = findQty.Qty - buynow.Qty;
                    _MasterContext.ProductBasics.Update(findQty);
                    await _MasterContext.SaveChangesAsync();
                }

                var ordersdetail = await _MasterContext.OrderMasters.FirstOrDefaultAsync(x => x.OrderGuid == orderMaster.OrderGuid && x.OrgId == orderMaster.OrgId);
                var StatusCheck = await _MasterContext.OrderStatuses.FirstOrDefaultAsync(x => x.OrderId == ordersdetail.OrderMasterId && x.OrgId == orderMaster.OrgId);
                if (StatusCheck == null)
                {
                    OrderStatus orderStatus = new OrderStatus();
                    orderStatus.OrderId = ordersdetail.OrderMasterId;
                    orderStatus.OrderStatus1 = "Pending";
                    orderStatus.StatusDate = DateTime.Now;
                    orderStatus.Remarks = string.Empty;
                    orderStatus.Insertby = DateTime.Now.ToString();
                    orderStatus.OrgId = orderMaster.OrgId;
                    _MasterContext.OrderStatuses.Add(orderStatus);
                    await _MasterContext.SaveChangesAsync();
                }               
                var OrderCheck = _MasterContext.OrderShippings.FirstOrDefault(x => x.OrderGuid == orderMaster.OrderGuid && x.OrgId == orders.orgid);
                var getemail = await _MasterContext.Users.FirstOrDefaultAsync(x => x.Username == getUsername.Username && x.OrgId == orders.orgid);        
                if(OrderCheck==null)
                {
                    OrderShipping shipping = new();
                    shipping.OrderGuid = orderMaster.OrderGuid;
                    shipping.UserName = getUsername.Username;
                    shipping.Contactnumber = orders.Contactnumber;
                    shipping.Phone = orders.Contactnumber;
                    shipping.Email = getemail.Email;
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
            else {
                var check = await _MasterContext.OrderBasics.Where(x => x.OrderGuid == orders.OrderGuid && x.OrderStatus == "Cart" && x.OrgId == orders.orgid).ToListAsync();
                OrderBasic ob = new();
                {
                    decimal? orderTotal = 0;

                    for (int i = 0; i < check.Count; i++)
                    {
                        for (int j = 0; j < orders.ProductLists.Count; j++)
                        {
                            if (check[i].OrderId == orders.ProductLists[j].OrderId)
                            {
                                check[i].OrderStatus = "Confirmed";
                                check[i].Qty = orders.ProductLists[j].Quantity;
                                check[i].InsertDate = DateTime.Now;
                                _MasterContext.OrderBasics.Update(check[i]);
                                await _MasterContext.SaveChangesAsync();
                                orderTotal += check[i].Price * check[i].Qty;
                                orders.BaseTotalPrice = orderTotal;
                            }
                        }
                    }
                }
                for (int i = 0; i < check.Count; i++)
                {
                    for (int j = 0; j < orders.ProductLists.Count; j++)
                    {
                        var findQty = _MasterContext.ProductBasics.FirstOrDefault(x => x.ProductId == orders.ProductLists[j].Id);
                        if (findQty != null)
                        {
                            if (check[i].ProductId == orders.ProductLists[j].Id)
                            {
                                findQty.Qty = findQty.Qty - orders.ProductLists[j].Quantity;
                                _MasterContext.ProductBasics.Update(findQty);
                                await _MasterContext.SaveChangesAsync();
                            }
                        }
                    }
                }
                foreach (var order in check)
                {
                    var ordersdetail = await _MasterContext.OrderMasters.FirstOrDefaultAsync(x => x.OrderGuid == order.OrderGuid && x.OrgId == order.OrgId);
                    var StatusCheck = await _MasterContext.OrderStatuses.FirstOrDefaultAsync(x => x.OrderId == ordersdetail.OrderMasterId && x.OrgId == order.OrgId);
                    if (StatusCheck == null)
                    {
                        OrderStatus orderStatus = new OrderStatus();
                        orderStatus.OrderId = ordersdetail.OrderMasterId;
                        orderStatus.OrderStatus1 = "Pending";
                        orderStatus.StatusDate = DateTime.Now;
                        orderStatus.Remarks = string.Empty;
                        orderStatus.Insertby = DateTime.Now.ToString();
                        orderStatus.OrgId = order.OrgId;
                        _MasterContext.OrderStatuses.Add(orderStatus);
                    }
                    await _MasterContext.SaveChangesAsync();
                }              
                    var OrderCheck = _MasterContext.OrderShippings.FirstOrDefault(x => x.OrderGuid == orders.OrderGuid && x.OrgId == orders.orgid);
                   // var getUsername = _MasterContext.OrderBasics.FirstOrDefault(x => x.OrderGuid == orders.OrderGuid && x.OrgId == orders.orgid);
                    var getemail = await _MasterContext.Users.FirstOrDefaultAsync(x => x.UserId == orders.UserId && x.OrgId == orders.orgid);
                    if (OrderCheck == null)
                    {
                        OrderShipping shipping = new();
                        shipping.OrderGuid = orders.OrderGuid;
                        shipping.UserName = getUsername.Username;
                        shipping.Contactnumber = orders.Contactnumber;
                        shipping.Phone = orders.Contactnumber;
                        shipping.Email = getemail.Email;
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
        public async Task<List<MyOrdersDTO>> GetMyOrderDetails(int OrgId, int UserId)
        {
            List<MyOrdersDTO> OrdersDTO = new();
            using (var command = this._MasterContext.Database.GetDbConnection().CreateCommand())
            {
                string strSQL = "SP_GetOrderDetails_User";
                command.CommandText = strSQL;
                command.CommandType = CommandType.StoredProcedure;
                var parameter = command.CreateParameter();
                command.Parameters.Add(new SqlParameter("@orgid", OrgId));
                command.Parameters.Add(new SqlParameter("@userid", UserId));
                await this._MasterContext.Database.OpenConnectionAsync();
                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        MyOrdersDTO myOrderDTO = new MyOrdersDTO();                      
                        myOrderDTO.orgId = Convert.ToInt32(OrgId);
                        myOrderDTO.orderId = Convert.ToInt32(result["OrderMasterId"]);
                        //myOrderDTO.OrderDate = (DateTime)result["OrderDate"];
                       // myOrderDTO.OrderStatus = result["OrderStatus"].ToString();
                        myOrderDTO.Price = Convert.ToDouble(result["Price"]);
                        myOrderDTO.userId = Convert.ToInt32(UserId);
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
                            orderListModel.OrderStatus = result["OrderStatus"].ToString();
                            orderListModel.UserId = Convert.ToInt32(result["UserId"]);
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
                    string strSQL = "SELECT Users.UserId,Users.OrgId, CONVERT(DATE, Order_Basic.InsertDate) AS OrderDate, " +
                                   "SUM(Order_Basic.Price*Order_Basic.QTY) As TotalPrice, " +
                                  "CONCAT(shiiping.Address + ''+',', shiiping.City + ''+',',shiiping.Street+ ''+',', shiiping.Zipcode) AS Address " +
                                  "FROM Order_Basic " +
                                  "Inner JOIN Users ON Order_Basic.UserName = Users.UserName " +
                                  "Inner JOIN Order_Shipping AS shiiping ON Order_Basic.OrderGuid = shiiping.OrderGuid " +
                                  "inner join Order_Master om on  Order_Basic.OrderGUID=om.OrderGUID " +
                                  "WHERE Order_Basic.OrgId= " + OrgId + " And om.OrderMasterId= " + OrderMasterId +
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
                            orderDetails.Address = result["Address"].ToString();
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
        public async Task<string> UpdateOrderStatus(Orders orders)
        {
            var orderStatus =await _MasterContext.OrderStatuses.FirstOrDefaultAsync(a => a.OrgId ==orders.orgId && a.OrderId==orders.OrderId);
            if(orderStatus!=null)
            {
                orderStatus.Remarks = orders.Remark;
                orderStatus.OrderStatus1 = orders.orderstatus;
                _MasterContext.Entry(orderStatus).State = EntityState.Detached;
                _MasterContext.Entry(orderStatus).State = EntityState.Modified;
                await _MasterContext.SaveChangesAsync();
                return "Success";
            }
            else
            {
                return "Something went Wrong..";
            }
        }
        public async Task<string> cancelOrder(CancelOrders orders)
        {
            var orderDetails = await _MasterContext.OrderStatuses.FirstOrDefaultAsync(a => a.OrgId == orders.orgId && a.OrderId == orders.OrderId);
            if(orderDetails.OrderStatus1=="Pending")
            {
                var orderMasterDetails = _MasterContext.OrderMasters.Where(u => u.OrderMasterId == orderDetails.OrderId && u.OrgId == orderDetails.OrgId).FirstOrDefault();
                var orderBasicdetails = await _MasterContext.OrderBasics.Where(o => o.OrderGuid == orderMasterDetails.OrderGuid && o.OrgId == orders.orgId).ToListAsync();
                if (orderDetails != null)
                {
                    orderDetails.Remarks = orders.Reason;
                    orderDetails.OrderStatus1 = "Cancelled";
                    _MasterContext.Entry(orderDetails).State = EntityState.Detached;
                    _MasterContext.Entry(orderDetails).State = EntityState.Modified;
                    await _MasterContext.SaveChangesAsync();

                    for (int i = 0; i < orderBasicdetails.Count; i++)
                    {
                        var productDetails = await _MasterContext.ProductBasics.FirstOrDefaultAsync(p => p.ProductId == orderBasicdetails[i].ProductId && p.OrgId == orders.orgId);
                        productDetails.Qty = productDetails.Qty + orderBasicdetails[i].Qty;
                        _MasterContext.Entry(productDetails).State = EntityState.Detached;
                        _MasterContext.Entry(productDetails).State = EntityState.Modified;
                        await _MasterContext.SaveChangesAsync();
                    }
                    return "Cancellation Confirmed";
                }
                else
                {
                    return "Something went Wrong.";
                }
            }
            else
            {
                return "Can't cancelled Your order";
            }          
        }
    }
}
