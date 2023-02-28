using System;
using System.Collections.Generic;

#nullable disable

namespace Shoppite.Core.Model
{
    public partial class WebsiteSetup
    {
        public int WebsiteSetupId { get; set; }
        public string ItemKey { get; set; }
        public string ItemName { get; set; }
        public decimal? ItemValue { get; set; }
        public string ItemDescription { get; set; }
        public bool? IsActive { get; set; }
        public string Type { get; set; }
        public string DeductionType { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UserName { get; set; }
        public int? OrgId { get; set; }
    }
}
