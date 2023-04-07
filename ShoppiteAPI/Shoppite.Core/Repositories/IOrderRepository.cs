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
       Task<List<OrderDetails>> GetOrderDetailsByOrgId(int OrgId);
    }
}
