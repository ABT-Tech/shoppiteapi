using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.DTOs
{
    public class ProductList
    {
        public int? Id { get; set; }
        public int OrderId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
    }
}
