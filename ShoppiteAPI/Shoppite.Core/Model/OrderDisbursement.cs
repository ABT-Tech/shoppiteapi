using System;
using System.Collections.Generic;

#nullable disable

namespace Shoppite.Core.Model
{
    public partial class OrderDisbursement
    {
        public int OrderEarningId { get; set; }
        public int? OrderId { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? DisburseDate { get; set; }
        public string DisbursementMode { get; set; }
        public string DisbursementId { get; set; }
        public string InsertBy { get; set; }
        public int? OrgId { get; set; }
    }
}
