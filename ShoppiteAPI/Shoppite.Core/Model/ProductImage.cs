using System;
using System.Collections.Generic;

#nullable disable

namespace Shoppite.Core.Model
{
    public partial class ProductImage
    {
        public int ProductImagesId { get; set; }
        public Guid? ProductGuid { get; set; }
        public string Image { get; set; }
        public string AltText { get; set; }
        public string Title { get; set; }
        public int? Displayorder { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UserName { get; set; }
        public int? OrgId { get; set; }
    }
}
