using System;
using System.Collections.Generic;

#nullable disable

namespace Shoppite.Core.Model
{
    public partial class UsersMembership
    {
        public int MembershipId { get; set; }
        public int? ProfileId { get; set; }
        public bool? IsFree { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? MembershipFee { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string PaymentMode { get; set; }
        public string PaymentStatus { get; set; }
        public string ReferenceId { get; set; }
        public string MembershipStatus { get; set; }
        public string CancelStatus { get; set; }
        public DateTime? Cancellationdate { get; set; }
        public int? OrgId { get; set; }
    }
}
