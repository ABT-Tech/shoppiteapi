using System;
using System.Collections.Generic;

#nullable disable

namespace Shoppite.Core.Model
{
    public partial class ProductBasic
    {
        public int ProductId { get; set; }
        public Guid? ProductGuid { get; set; }
        public string ProductName { get; set; }
        public string Urlpath { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Sku { get; set; }
        public int? Qty { get; set; }
        public DateTime? ProductStartDate { get; set; }
        public DateTime? ProductEndDate { get; set; }
        public bool? IsPublished { get; set; }
        public DateTime? InsertDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ProfileId { get; set; }
        public string CoverImage { get; set; }
        public int? OrgId { get; set; }
    }
}
