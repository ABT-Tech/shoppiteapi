using System;
using System.Collections.Generic;

#nullable disable

namespace Shoppite.Core.Model
{
    public partial class ProductTag
    {
        public int ProductTagsId { get; set; }
        public Guid? ProductGuid { get; set; }
        public string ProductTags { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UserName { get; set; }
        public int? OrgId { get; set; }
    }
}
