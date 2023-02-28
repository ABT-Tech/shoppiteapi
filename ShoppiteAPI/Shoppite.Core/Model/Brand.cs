using System;
using System.Collections.Generic;

#nullable disable

namespace Shoppite.Core.Model
{
    public partial class Brand
    {
        public int BrandId { get; set; }
        public int? CategoryId { get; set; }
        public string BrandName { get; set; }
        public string BrandDescription { get; set; }
        public string BrandImage { get; set; }
        public string BrandUrlpath { get; set; }
        public DateTime? InsertDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsPublished { get; set; }
        public string UserName { get; set; }
        public int? OrgId { get; set; }
    }
}
