using System;
using System.Collections.Generic;

#nullable disable

namespace Shoppite.Core.Model
{
    public partial class ProductCategory
    {
        public int ProductCategoryId { get; set; }
        public Guid? ProductGuid { get; set; }
        public int? CategoryId { get; set; }
        public int? MainCatId { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UserName { get; set; }
        public int? OrgId { get; set; }
    }
}
