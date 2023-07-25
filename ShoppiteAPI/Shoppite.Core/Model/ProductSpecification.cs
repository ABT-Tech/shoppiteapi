using System;
using System.Collections.Generic;

#nullable disable

namespace Shoppite.Core.Model
{
    public partial class ProductSpecification
    {
        public int ProductSpecificationId { get; set; }
        public Guid? ProductGuid { get; set; }
        public int? SpecificationId { get; set; }
        public string ControlType { get; set; }
        public decimal? Price { get; set; }
        public string Image { get; set; }
        public DateTime? Insertdate { get; set; }
        public string UserName { get; set; }
        public int? OrgId { get; set; }
        public int? Quantity { get; set; }
    }
}
