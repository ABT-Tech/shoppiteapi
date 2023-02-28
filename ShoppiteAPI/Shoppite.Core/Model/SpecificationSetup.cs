using System;
using System.Collections.Generic;

#nullable disable

namespace Shoppite.Core.Model
{
    public partial class SpecificationSetup
    {
        public int SpecificationId { get; set; }
        public int? AttributeId { get; set; }
        public string SpecificationName { get; set; }
        public string SpecificiatoinDescription { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UserName { get; set; }
        public int? ProfileId { get; set; }
        public decimal? Price { get; set; }
        public int? OrgId { get; set; }
    }
}
