using System;
using System.Collections.Generic;

#nullable disable

namespace Shoppite.Core.Model
{
    public partial class EmailSetup
    {
        public int EmailSettingId { get; set; }
        public string EmailFrom { get; set; }
        public string Password { get; set; }
        public int? Smtpport { get; set; }
        public string Host { get; set; }
        public string Bcc { get; set; }
        public string Type { get; set; }
        public int? OrgId { get; set; }
    }
}
