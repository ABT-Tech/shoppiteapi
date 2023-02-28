using System;
using System.Collections.Generic;

#nullable disable

namespace Shoppite.Core.Model
{
    public partial class ProductPrice
    {
        public int ProductPriceId { get; set; }
        public Guid? ProductGuid { get; set; }
        public int? CurrencyId { get; set; }
        public decimal? Price { get; set; }
        public decimal? OldPrice { get; set; }
        public bool? DisableBuyButton { get; set; }
        public bool? TaxExempt { get; set; }
        public decimal? DeliveryFees { get; set; }
        public int? OrgId { get; set; }
    }
}
