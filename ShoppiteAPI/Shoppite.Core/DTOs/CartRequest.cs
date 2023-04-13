using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.DTOs
{
    public class CartRequest
    {
        public int UserId { get; set; }
        public int orgId { get; set; }
        public int proId { get; set; }
        public int? Qty { get; set; }
    }
}
