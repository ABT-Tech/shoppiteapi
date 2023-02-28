using System;
using System.Collections.Generic;

#nullable disable

namespace Shoppite.Core.Model
{
    public partial class ProductRecentlyViewed
    {
        public int RecentlyViewId { get; set; }
        public int? ProductId { get; set; }
        public string Ip { get; set; }
        public DateTime? Insertdate { get; set; }
        public int? OrgId { get; set; }
    }
}
