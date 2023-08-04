using Shoppite.Core.DTOs;
using Shoppite.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.Repositories
{
    public interface IOrderRepository
    {
       Task BuyNow(OrdersDTO order);
       Task<List<MyOrdersDTO>> GetMyOrderDetails(int OrgId, int UserId);
       Task<OrderDetails> GetOrderDetailsByOrgId(int OrgId,int OrderMasterId);
       Task<List<VendorsOrder>> GetOrdersDetailForVendor(int OrgId);
       Task<string> UpdateOrderStatus(Orders orders);
       Task<string> cancelOrder(CancelOrders orders);
       Task<List<Report>> GetTotalOrderDetails(int OrgId);
       Task<OrdersDTO> GetOnePayFlag(int OrgId);
       Task<PaymentGatewayResponse> MakePaymentRequest(OrdersDTO orders);
    }
}
