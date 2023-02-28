using System;
using System.Collections.Generic;

#nullable disable

namespace Shoppite.Core.Model
{
    public partial class ProductDiscount
    {
        public int DiscountId { get; set; }
        public string DiscountType { get; set; }
        public decimal? DiscountOffer { get; set; }
        public DateTime? DiscountStartDate { get; set; }
        public DateTime? DiscountEndDate { get; set; }
        public string ModuleType { get; set; }
        public string ModuleId { get; set; }
        public DateTime? InsertDate { get; set; }
        public bool? IsActive { get; set; }
        public int? OrgId { get; set; }
    }
}
