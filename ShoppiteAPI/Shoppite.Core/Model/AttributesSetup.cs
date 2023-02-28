using System;
using System.Collections.Generic;

#nullable disable

namespace Shoppite.Core.Model
{
    public partial class AttributesSetup
    {
        public int AttributeId { get; set; }
        public string AttributeName { get; set; }
        public string AttributeDescription { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UserName { get; set; }
        public int? ProfileId { get; set; }
        public int? OrgId { get; set; }
    }
}
