using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.DTOs
{
    public class AllProductsDTO
    {
        public string Title { get; set; }
        public int Quantity { get; set; }
        public string Brand { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public int? Id { get; set; }
        public int orgId { get; set; }
        public Guid ProductGUID { get; set; }
        public double OldPrice { get; set; }
        public string SpecificationNames { get; set; }
        public int SpecificationId { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public string Status { get; set; }      
        
    }
}
