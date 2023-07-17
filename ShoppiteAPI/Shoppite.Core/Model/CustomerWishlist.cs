using System;
using System.Collections.Generic;

#nullable disable

namespace Shoppite.Core.Model
{
    public partial class CustomerWishlist
    {
        public int WishlistId { get; set; }
        public int? ProductId { get; set; }
        public string UserName { get; set; }
        public DateTime? InsertDate { get; set; }
        public string Ip { get; set; }
        public int? OrgId { get; set; }
        public int ProductSpecificationId { get; set; }
    }
}
