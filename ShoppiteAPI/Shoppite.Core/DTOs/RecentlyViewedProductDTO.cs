using System;

namespace Shoppite.Core.DTOs
{
    public class RecentlyViewedProductDTO
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public Guid? ProductGuid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Ip { get; set; }
        public int? OrgId { get; set; }
        public double Price { get; set; }
        public double OldPrice { get; set; }
        public string Brand { get; set; }       
        public DateTime productviewinsertdate { get; set; }
        public string STATUS { get; set; }
        public int Quantity { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }

    }
}
