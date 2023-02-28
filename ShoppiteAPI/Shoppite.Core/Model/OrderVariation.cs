using System;
using System.Collections.Generic;

#nullable disable

namespace Shoppite.Core.Model
{
    public partial class OrderVariation
    {
        public int OrderVariationId { get; set; }
        public Guid? OrderGuid { get; set; }
        public int? ProductSpecificationId { get; set; }
        public int? OrgId { get; set; }
    }
}
