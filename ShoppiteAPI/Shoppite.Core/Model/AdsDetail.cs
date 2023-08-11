using System;
using System.Collections.Generic;

#nullable disable

namespace Shoppite.Core.Model
{
    public partial class AdsDetail
    {
        public int AdId { get; set; }
        public int? AdsPlacementId { get; set; }
        public int? AdsPageId { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }
        public string UserName { get; set; }
        public DateTime? InsertDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? OrgId { get; set; }
        public int CategoryId { get; set; }
    }
}
