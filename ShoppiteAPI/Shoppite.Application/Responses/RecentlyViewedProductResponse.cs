using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Application.Responses
{
    public class RecentlyViewedProductResponse
    {
        public int ProductId { get; set; }
        public string Image { get; set; }
        public Guid? ProductGuid { get; set; }
        public string ProductName { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Sku { get; set; }
        public string maincategoryname { get; set; }
        public bool? IsPublished { get; set; }
        public int? ProfileId { get; set; }
        public int? OrgId { get; set; }
        public int Category_Id { get; set; }
        public string Category_Name { get; set; }
        public double Price { get; set; }
        public double OldPrice { get; set; }
        public string Brands { get; set; }
        public int BrandId { get; set; }
        public int? MainCatId { get; set; }
        public DateTime productviewinsertdate { get; set; }
        public string STATUS { get; set; }
        public int NumOfViews { get; set; }
        public int Quantity { get; set; }




    }
}
