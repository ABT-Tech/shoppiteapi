using System;
using System.Collections.Generic;

#nullable disable

namespace Shoppite.Core.Model
{
    public partial class NewsLetter
    {
        public int NewsletterId { get; set; }
        public string Email { get; set; }
        public DateTime? InsertDate { get; set; }
        public int? OrgId { get; set; }
    }
}
