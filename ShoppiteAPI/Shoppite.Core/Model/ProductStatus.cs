using System;
using System.Collections.Generic;

#nullable disable

namespace Shoppite.Core.Model
{
    public partial class ProductStatus
    {
        public int ProductStatusId { get; set; }
        public Guid? ProductGuid { get; set; }
        public int? StatusId { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UserName { get; set; }
        public int? OrgId { get; set; }
    }
}
