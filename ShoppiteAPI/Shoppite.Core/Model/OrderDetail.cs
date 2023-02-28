using System;
using System.Collections.Generic;

#nullable disable

namespace Shoppite.Core.Model
{
    public partial class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public Guid? OrderGuid { get; set; }
        public decimal? DeliveryFees { get; set; }
        public decimal? TotalOrderAmount { get; set; }
        public int? OrgId { get; set; }
    }
}
