using System;
using System.Collections.Generic;

#nullable disable

namespace Shoppite.Core.Model
{
    public partial class AdsPageName
    {
        public int AdsPageId { get; set; }
        public int? AdsPlacementId { get; set; }
        public string PageName { get; set; }
        public string Status { get; set; }
        public int? OrgId { get; set; }
    }
}
