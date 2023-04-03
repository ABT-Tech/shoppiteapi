using System;

namespace Shoppite.Core.DTOs
{
    public class RecentlyViewedProductDTO
    {
        public int ProductId { get; set; }
        public string Image { get; set; }
        public Guid? ProductGuid { get; set; }
        public string ProductName { get; set; }
        public string Urlpath { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Sku { get; set; }
        public string maincategoryname { get; set; }
        public bool? IsPublished { get; set; }
        public int? ProfileId { get; set; }
        public string Ip { get; set; }
        public DateTime? Insertdate { get; set; }
        public int? OrgId { get; set; }
        public int Category_Id { get; set; }
        public string Category_Name { get; set; }
        public string CategoryUrlPath { get; set; }
        public double Price { get; set; }
        public double OldPrice { get; set; }
        public string Brands { get; set; }
        public int BrandId { get; set; }
        public string brandsUrlpath { get; set; }
        public int? MainCatId { get; set; }
        public string maincaturlpath { get; set; }
        public DateTime productviewinsertdate { get; set; }
        public string STATUS { get; set; }
        public int NumOfViews { get; set; }
    }
}
