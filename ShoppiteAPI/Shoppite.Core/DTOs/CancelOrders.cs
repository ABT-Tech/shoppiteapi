using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.DTOs
{
    public class CancelOrders
    {
        public int OrderId { get; set; }
        public int orgId { get; set; }
        public string Reason { get; set; }
    }
}
