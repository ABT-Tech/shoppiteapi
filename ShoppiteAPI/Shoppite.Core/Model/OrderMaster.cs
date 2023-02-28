using System;
using System.Collections.Generic;

#nullable disable

namespace Shoppite.Core.Model
{
    public partial class OrderMaster
    {
        public Guid OrderGuid { get; set; }
        public DateTime? InsertDate { get; set; }
        public string OrderKeyStatus { get; set; }
        public int? OrgId { get; set; }
    }
}
