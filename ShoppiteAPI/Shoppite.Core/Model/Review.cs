using System;
using System.Collections.Generic;

#nullable disable

namespace Shoppite.Core.Model
{
    public partial class Review
    {
        public int ReviewId { get; set; }
        public int? Star { get; set; }
        public string Comments { get; set; }
        public int? TransactionId { get; set; }
        public string Module { get; set; }
        public string UserName { get; set; }
        public DateTime? InsertDate { get; set; }
        public int? OrgId { get; set; }
    }
}
