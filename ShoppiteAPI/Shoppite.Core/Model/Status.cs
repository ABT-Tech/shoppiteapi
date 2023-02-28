using System;
using System.Collections.Generic;

#nullable disable

namespace Shoppite.Core.Model
{
    public partial class Status
    {
        public int StatusId { get; set; }
        public string Status1 { get; set; }
        public string Urlpath { get; set; }
        public bool? ShowOnFront { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UserName { get; set; }
        public string CssClass { get; set; }
        public int? OrgId { get; set; }
    }
}
