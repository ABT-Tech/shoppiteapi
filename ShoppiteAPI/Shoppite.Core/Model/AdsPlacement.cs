using System;
using System.Collections.Generic;

#nullable disable

namespace Shoppite.Core.Model
{
    public partial class AdsPlacement
    {
        public int AdsPlacementId { get; set; }
        public string PlacementName { get; set; }
        public string Description { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public string Status { get; set; }
        public int? OrgId { get; set; }
    }
}
