using System;
using System.Collections.Generic;

#nullable disable

namespace Shoppite.Core.Model
{
    public partial class ProductBrand
    {
        public int ProductBrandId { get; set; }
        public Guid? ProductGuid { get; set; }
        public int? BrandId { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UserName { get; set; }
        public int? OrgId { get; set; }
    }
}
