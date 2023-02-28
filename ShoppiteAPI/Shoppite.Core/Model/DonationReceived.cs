using System;
using System.Collections.Generic;

#nullable disable

namespace Shoppite.Core.Model
{
    public partial class DonationReceived
    {
        public int DonationReceivedId { get; set; }
        public Guid? RequestFundGuid { get; set; }
        public decimal? Amount { get; set; }
        public decimal? AdministrativeFeesPer { get; set; }
        public decimal? AdministrativeAmount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string PaypalId { get; set; }
        public int? OrgId { get; set; }
    }
}
