using System;
using System.Collections.Generic;

#nullable disable

namespace Shoppite.Core.Model
{
    public partial class WebsiteSetupScript
    {
        public int Scriptid { get; set; }
        public string Title { get; set; }
        public string Scriptname { get; set; }
        public string Type { get; set; }
        public int? OrgId { get; set; }
    }
}
