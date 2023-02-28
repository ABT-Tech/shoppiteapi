using System;
using System.Collections.Generic;

#nullable disable

namespace Shoppite.Core.Model
{
    public partial class OrderStatus
    {
        public int OrderStatusId { get; set; }
        public int? OrderId { get; set; }
        public string OrderStatus1 { get; set; }
        public DateTime? StatusDate { get; set; }
        public string Remarks { get; set; }
        public string Insertby { get; set; }
        public int? OrgId { get; set; }
    }
}
