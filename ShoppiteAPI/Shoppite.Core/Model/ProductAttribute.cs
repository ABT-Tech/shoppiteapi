using System;
using System.Collections.Generic;

#nullable disable

namespace Shoppite.Core.Model
{
    public partial class ProductAttribute
    {
        public int ProductAttributeId { get; set; }
        public Guid? ProductGuid { get; set; }
        public int? AttributeId { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UserName { get; set; }
        public int? OrgId { get; set; }
    }
}
