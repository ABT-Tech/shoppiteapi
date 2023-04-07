using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.DTOs.Mapper
{
    public class VendorsOrder
    {
        public int orgId { get; set; }
        public int userId { get; set; }
        public int orderId { get; set; }
        public double Price { get; set; }
    }
    
}
