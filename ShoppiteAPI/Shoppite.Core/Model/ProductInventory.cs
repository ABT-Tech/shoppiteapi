using System;
using System.Collections.Generic;

#nullable disable

namespace Shoppite.Core.Model
{
    public partial class ProductInventory
    {
        public int ProductInventoryId { get; set; }
        public Guid? ProductGuid { get; set; }
        public int? Inventory { get; set; }
        public DateTime? InsertDate { get; set; }
        public int? OrgId { get; set; }
    }
}
