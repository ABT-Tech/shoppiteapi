using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.DTOs
{
    public class ProductsDTO
    {
        public string Title { get; set; }
        public string Brand { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int? Id { get; set; }
        public int orgId { get; set; }
        public string[] ProductList { get; set; }
        public double OldPrice { get; set; }
    }
}
