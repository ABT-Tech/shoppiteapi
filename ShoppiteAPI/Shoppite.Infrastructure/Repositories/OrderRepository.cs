using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Shoppite.Core.DTOs;
using Shoppite.Core.Model;
using Shoppite.Core.Repositories;
using Shoppite.Infrastructure.Data;
using Shoppite.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        protected readonly Shoppite_masterContext _MasterContext;
        protected readonly IConfiguration _configuration;
        public OrderRepository(Shoppite_masterContext dbContext,IConfiguration configuration)
        {
            _MasterContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _configuration = configuration;
        }
        public async Task<string> BuyNow(OrdersDTO orders)
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
                               
                OrderVariation variation = new();
                variation.OrderGuid = buynow.OrderGuid;
                variation.OrgId = buynow.OrgId;
                if (orders.ProductLists[p].SpecificationId != 0)
                {
                    var getspecId = await _MasterContext.ProductSpecifications.FirstOrDefaultAsync(x => x.SpecificationId == orders.ProductLists[p].SpecificationId && x.OrgId == orders.orgid && x.ProductGuid == productDetail.ProductGuid);
                    variation.ProductSpecificationId = getspecId.ProductSpecificationId;
                }               

                _MasterContext.OrderVariations.Add(variation);
                await _MasterContext.SaveChangesAsync();

                var findQty = _MasterContext.ProductBasics.FirstOrDefault(x => x.ProductId == buynow.ProductId && x.OrgId == orders.orgid);
                if (orders.ProductLists[p].SpecificationId == 0)
                {
                    if (findQty != null)
                    {
                        findQty.Qty = findQty.Qty - buynow.Qty;
                        _MasterContext.ProductBasics.Update(findQty);
                        await _MasterContext.SaveChangesAsync();
                    }
                }
                else
                {      
                    var SpecQty = _MasterContext.ProductSpecifications.FirstOrDefault(x => x.ProductGuid == productDetail.ProductGuid&&x.SpecificationId==orders.ProductLists[p].SpecificationId&&x.OrgId==orders.orgid);
                    if(SpecQty == null)
                    {
                        if (findQty != null)
                        {
                            findQty.Qty -= buynow.Qty;
                            _MasterContext.ProductBasics.Update(findQty);
                            await _MasterContext.SaveChangesAsync();
                        }
                    }
                    else
                    {
                        SpecQty.Quantity -= buynow.Qty;
                        _MasterContext.ProductSpecifications.Update(SpecQty);
                        await _MasterContext.SaveChangesAsync();
                    }
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
                if (orders.CoupanId != null)
                {
                    var getCoupandetails = await _MasterContext.User_Coupans.FirstOrDefaultAsync(uc => uc.UserId == orders.UserId && uc.OrgId == orders.orgid && uc.CoupanId == orders.CoupanId);
                    var CouponAplliedCount = await _MasterContext.User_Coupans.Where(uc => uc.UserId == orders.UserId && uc.CoupanId == orders.CoupanId).ToListAsync();
                    if (getCoupandetails != null)
                    {
                        return "You Have reached Maximum Limit Try It in Another Shop!";
                        // var getCoupanUserDetails=
                    }
                    else if (CouponAplliedCount.Count <= 5)
                    {
                        User_Coupan user_Coupan = new();
                        user_Coupan.CoupanId = orders.CoupanId;
                        user_Coupan.UserId = orders.UserId;
                        user_Coupan.CreatedAt = DateTime.Now;
                        user_Coupan.OrgId = (int)orders.orgid;
                        _MasterContext.User_Coupans.Add(user_Coupan);
                        await _MasterContext.SaveChangesAsync();
                    }
                    else
                    {
                        return "Sorry,You Have reached Maximum Limit!";
                    }
                }
            }
            else
            {
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
                        if (orders.ProductLists[j].SpecificationId == 0 || orders.ProductLists[j].SpecificationId == null) 
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
                        else
                        {
                            var findQty = _MasterContext.ProductBasics.FirstOrDefault(x => x.ProductId == orders.ProductLists[j].Id);
                            var SpecQty = _MasterContext.ProductSpecifications.FirstOrDefault(x => x.SpecificationId == orders.ProductLists[j].SpecificationId&&x.ProductGuid==findQty.ProductGuid);
                            var OrderVariation = _MasterContext.OrderVariations.FirstOrDefault(o=>o.OrderGuid==orders.OrderGuid&&o.OrderId==check[i].OrderId);
                            if (findQty != null)
                            {
                                if (check[i].ProductId == orders.ProductLists[j].Id&&SpecQty.ProductSpecificationId== OrderVariation.ProductSpecificationId)
                                {
                                    if (SpecQty.Quantity == null)
                                    {
                                        findQty.Qty = findQty.Qty - orders.ProductLists[j].Quantity;
                                        _MasterContext.ProductBasics.Update(findQty);
                                        await _MasterContext.SaveChangesAsync();
                                    }
                                    else
                                    {
                                        SpecQty.Quantity = SpecQty.Quantity - orders.ProductLists[j].Quantity;
                                        _MasterContext.ProductSpecifications.Update(SpecQty);
                                        await _MasterContext.SaveChangesAsync();
                                    }
                                }
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
                if(orders.CoupanId != null)
                {
                    var getCoupandetails = await _MasterContext.User_Coupans.FirstOrDefaultAsync(uc=>uc.UserId==orders.UserId&&uc.OrgId==orders.orgid&&uc.CoupanId==orders.CoupanId);
                    var  CouponAplliedCount= await _MasterContext.User_Coupans.Where(uc => uc.UserId == orders.UserId && uc.CoupanId == orders.CoupanId).ToListAsync();
                    if (getCoupandetails!=null)
                    {
                        return "You Have reached Maximum Limit Try It in Another Shop!";
                        // var getCoupanUserDetails=
                    }
                    else if(CouponAplliedCount.Count<=5)
                    {
                        User_Coupan user_Coupan = new();
                        user_Coupan.CoupanId = orders.CoupanId;
                        user_Coupan.UserId = orders.UserId;
                        user_Coupan.CreatedAt = DateTime.Now;
                        user_Coupan.OrgId =(int)orders.orgid;
                        _MasterContext.User_Coupans.Add(user_Coupan);
                        await _MasterContext.SaveChangesAsync();
                    }
                    else
                    {
                        return "Sorry,You Have reached Maximum Limit!";
                    }
                    
                }
              //  return "Suceess";
            }
            return "Success";
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
                        myOrderDTO.OrderStatus = result["OrderStatus"].ToString();
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
                            orderListModel.SpecificationNames = result["SpecificationNames"].ToString();
                            orderListModel.SpecificationIds = Convert.ToInt32(result["SpecificationIds"]);
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
                                  "LEFT JOIN Users ON Order_Basic.UserName = Users.Email " +
                                  "LEFT JOIN Order_Shipping AS shiiping ON Order_Basic.OrderGuid = shiiping.OrderGuid " +
                                  "LEFT JOIn Order_Variation As ov ON Order_Basic.OrgId=ov.OrgId and ov.OrderGuid=Order_Basic.OrderGuid " +
                                  "and Order_Basic.OrderId=ov.OrderId " +
                                  "LEFT join Order_Master om on  Order_Basic.OrderGUID=om.OrderGUID " +
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
                        var OrderVariation =  _MasterContext.OrderVariations.Where(ov => ov.OrderGuid == orderMasterDetails.OrderGuid && ov.OrderId == orderBasicdetails[i].OrderId).FirstOrDefault();
                        var productDetails = await _MasterContext.ProductBasics.FirstOrDefaultAsync(p => p.ProductId == orderBasicdetails[i].ProductId && p.OrgId == orders.orgId);
                        if (OrderVariation.ProductSpecificationId == 0||OrderVariation.ProductSpecificationId==null)
                        {
                            productDetails.Qty += orderBasicdetails[i].Qty;
                            _MasterContext.Entry(productDetails).State = EntityState.Detached;
                            _MasterContext.Entry(productDetails).State = EntityState.Modified;
                        }
                        else
                        {
                            var productspecDetails = await _MasterContext.ProductSpecifications.FirstOrDefaultAsync(p => p.ProductGuid == productDetails.ProductGuid&& p.ProductSpecificationId==OrderVariation.ProductSpecificationId && p.OrgId == orders.orgId);
                            if(productspecDetails.Quantity!=null)
                            {
                                productspecDetails.Quantity = productspecDetails.Quantity + orderBasicdetails[i].Qty;
                                _MasterContext.Entry(productspecDetails).State = EntityState.Detached;
                                _MasterContext.Entry(productspecDetails).State = EntityState.Modified;
                            }
                            else
                            {
                                productDetails.Qty = productDetails.Qty + orderBasicdetails[i].Qty;
                                _MasterContext.Entry(productDetails).State = EntityState.Detached;
                                _MasterContext.Entry(productDetails).State = EntityState.Modified;
                            }
                        }
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
        public async Task<List<Report>> GetTotalOrderDetails(int OrgId)
        {
            List<Report> details = new();
            using (var command = this._MasterContext.Database.GetDbConnection().CreateCommand())
            {
                string strSQL = "SP_CustomerReport";
                command.CommandText = strSQL;
                command.CommandType = CommandType.StoredProcedure;
                var parameter = command.CreateParameter();
                command.Parameters.Add(new SqlParameter("@OrgId", OrgId));
                await this._MasterContext.Database.OpenConnectionAsync();
                using var result = await command.ExecuteReaderAsync();
                while (await result.ReadAsync())
                {
                    Report report = new();
                    report.UserName = result["UserName"].ToString();
                    report.TtlOrder = Convert.ToInt32(result["TtlOrder"]);
                    report.orgId = Convert.ToInt32(OrgId);
                    report.userId = Convert.ToInt32(result["userId"]);
                    report.Date = result["LastOrderDate"]!= DBNull.Value ? Convert.ToDateTime(result["LastOrderDate"]).ToString("dd/MM/yyyy") : "";
                    if (report.TtlOrder != 0) { details.Add(report); }
                }
            }           
            return details;
        }
        public async Task<OrdersDTO> GetOnePayFlag(int OrgId)
        {
            OrdersDTO orderDetails = new();
            using (var command = this._MasterContext.Database.GetDbConnection().CreateCommand())
            {
                string strSQL = "SELECT ORGANIZATION.ID FROM ORGANIZATION " +
                    "JOIN Organization_Aggregator_Control ON ORGANIZATION.ID=Organization_Aggregator_Control.ORGID " +
                    "WHERE ORGID= " +OrgId;
                    
                command.CommandText = strSQL;
                command.CommandType = CommandType.Text;
                var parameter = command.CreateParameter();
                await this._MasterContext.Database.OpenConnectionAsync();
                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        if (OrgId != 0|| OrgId!=null)
                        {
                            orderDetails.OnePay = true;
                        }
                        else
                        {
                            orderDetails.OnePay = false;
                        }

                    }
                }
            }
            return orderDetails;
        }
        public async Task<PaymentGatewayResponse> MakePaymentRequest(OrdersDTO orders)
        {
            PaymentGatewayResponse response = new();
            CartRepository cartRepository = new CartRepository(_MasterContext);
            if (orders.OrderGuid == null || orders.OrderGuid == Guid.Empty)
            {
                foreach (var product in orders.ProductLists)
                {
                    CartRequest cartRequest = new CartRequest();
                    cartRequest.orgId = (int)orders.orgid;
                    cartRequest.proId = (int)product.Id;
                    cartRequest.Qty = (int)product.Quantity;
                    cartRequest.SpecificationId = (int)product.SpecificationId;
                    cartRequest.UserId = (int)orders.UserId;
                    await cartRepository.AddToCart(cartRequest);
                }
                var username = await _MasterContext.Users.FirstOrDefaultAsync(u => u.UserId == orders.UserId && u.OrgId == orders.orgid);
                var orderData = await _MasterContext.OrderBasics.FirstOrDefaultAsync(u => u.UserName == username.Email && u.OrgId == orders.orgid && u.OrderStatus == "Cart");
                orders.OrderGuid = orderData.OrderGuid;
            }
            
            if (orders.OnePay)
            {
                var getUsername = await _MasterContext.Users.FirstOrDefaultAsync(u => u.UserId == orders.UserId && u.OrgId == orders.orgid);
                var getUserProfile = await _MasterContext.UsersProfiles.FirstOrDefaultAsync(u => u.ProfileGuid == getUsername.Guid);
                var order = orders.OrderGuid;
                var merchantDetails = _MasterContext.Organization_Aggregator_Controls.FirstOrDefault(x => x.OrgId == orders.orgid);
                var strProductMapping = string.Empty;
                decimal? TotalOrderCharge = 0;
                decimal? TotalProductCharges = 0;
                var IsTestEnable = _configuration.GetSection("OnePeSettings")["IsTest"].ToString();
                response.AggregatorRedirectionLink = _configuration.GetSection("OnePeSettings")["URL"];
             

                foreach (var orderDetails in orders.ProductLists)
                {
                    TotalProductCharges = orderDetails.Price+ orderDetails.DeliveryFees; ;
                    if (string.IsNullOrEmpty(strProductMapping))
                        strProductMapping += orderDetails.Id + "~" + TotalProductCharges;
                    else
                        strProductMapping += "|" + orderDetails.Id + "~" + TotalProductCharges;
                    TotalOrderCharge = TotalOrderCharge + TotalProductCharges;
                }
                using (HttpClient client = new HttpClient())
                {
                    MerchantParams merchantParams = new MerchantParams();
                    merchantParams.merchantId = merchantDetails.AggregatorMerchantId;
                    merchantParams.apiKey = merchantDetails.AggregatorMerchantApiKey;
                    merchantParams.txnId = orders.OrderGuid.ToString();
                    merchantParams.amount = IsTestEnable == "1" ? "10.00" : TotalOrderCharge.ToString();
                    merchantParams.dateTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    merchantParams.custMail = getUsername.Email;
                    merchantParams.custMobile = getUserProfile.ContactNumber;
                    merchantParams.udf1 = orders.Address.AddressDetail;
                    merchantParams.udf2 = orders.Address.AddressDetail;
                    merchantParams.returnURL = merchantDetails.AggregatorCallbackURL + "Cart/PaymentResponse";
                    merchantParams.isMultiSettlement = "0";
                    merchantParams.productId = "DEFAULT";
                    merchantParams.channelId = "0";
                    merchantParams.txnType = "DIRECT";
                    merchantParams.Rid = merchantDetails.AggregatorRID.ToString();
                    var objMerchantParams = JsonConvert.SerializeObject(merchantParams);
                    EncryptionHelper encryptionHelper = new EncryptionHelper();
                    string encryptedParams = encryptionHelper.EncryptPaymentRequest(merchantDetails.AggregatorMerchantId, merchantDetails.AggregatorMerchantApiKey, objMerchantParams);
                    response.merchantId = merchantParams.merchantId;
                    response.AggregatorCallbackURL = merchantDetails.AggregatorCallbackURL;
                    response.encryptedParams = encryptedParams;
                }                                          
            }         
            return response;          
        }
    }
}
