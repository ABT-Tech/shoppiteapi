using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Application.Responses
{
    public class MyOrderResponse
    {
        public int orgId { get; set; }
        public int userId { get; set; }
        public int orderId { get; set; }
        public string OrderStatus { get; set; }
        public double Price { get; set; }
    }
}
