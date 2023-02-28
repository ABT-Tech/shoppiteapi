using System;
using System.Collections.Generic;

#nullable disable

namespace Shoppite.Core.Model
{
    public partial class PageCategoryDetail
    {
        public int PageCategoryDetailId { get; set; }
        public int? PageCategoryId { get; set; }
        public string PageCategoryDescriptoin { get; set; }
        public string Status { get; set; }
        public DateTime? InsertDate { get; set; }
        public int? OrgId { get; set; }
    }
}
