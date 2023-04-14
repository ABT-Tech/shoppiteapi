using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.DTOs
{
    public class Orders
    {
        public int UserId { get; set; }
        public int OrderId { get; set; }
        public int orgId { get; set; }
        public string Remark { get; set; }
        public string orderstatus { get; set; }
    }
}
