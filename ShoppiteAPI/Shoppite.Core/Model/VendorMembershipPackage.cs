using System;
using System.Collections.Generic;

#nullable disable

namespace Shoppite.Core.Model
{
    public partial class VendorMembershipPackage
    {
        public int Membershipid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Membershiptype { get; set; }
        public int? RecurringPeriod { get; set; }
        public int? CurrencyId { get; set; }
        public decimal? Fees { get; set; }
        public string Unit { get; set; }
        public DateTime? InsertDate { get; set; }
        public bool? IsActive { get; set; }
        public int? Sortoption { get; set; }
        public int? OrgId { get; set; }
    }
}
