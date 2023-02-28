using System;
using System.Collections.Generic;

#nullable disable

namespace Shoppite.Core.Model
{
    public partial class ProductSeo
    {
        public int Seoid { get; set; }
        public Guid? ProductGuid { get; set; }
        public string SeoPageName { get; set; }
        public string SeoMetaTitle { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoMetadescription { get; set; }
        public int? OrgId { get; set; }
    }
}
