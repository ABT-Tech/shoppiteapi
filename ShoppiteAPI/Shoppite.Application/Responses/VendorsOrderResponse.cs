using Shoppite.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Application.Responses
{
    public class VendorsOrderResponse
    {
        public int orgId { get; set; }
        public int userId { get; set; }
        public int orderId { get; set; }
        public double Price { get; set; }
    }
}
