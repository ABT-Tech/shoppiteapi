using System;
using System.Collections.Generic;

#nullable disable

namespace Shoppite.Core.Model
{
    public partial class DonationMaster
    {
        public int RequestFundId { get; set; }
        public Guid? RequestFundGuid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Administrativefee { get; set; }
        public bool? IsActive { get; set; }
        public string UserName { get; set; }
        public DateTime? InsertDate { get; set; }
        public string PaypalId { get; set; }
        public bool? AdminStatus { get; set; }
        public string AdminRemarks { get; set; }
        public int? OrgId { get; set; }
    }
}
