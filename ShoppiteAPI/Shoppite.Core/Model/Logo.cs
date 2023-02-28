﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Shoppite.Core.Model
{
    public partial class Logo
    {
        public int LogoId { get; set; }
        public string Logo1 { get; set; }
        public int? Logowidth { get; set; }
        public int? Logoheight { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string SupportContact { get; set; }
        public string WebsiteName { get; set; }
        public string FooterLogo { get; set; }
        public string Favicon { get; set; }
        public string Title { get; set; }
        public string Keyword { get; set; }
        public string Description { get; set; }
        public int? OrgId { get; set; }
    }
}
