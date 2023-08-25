using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Application.Responses
{
    public class CartResponse
    {
        public string Title { get; set; }
        public string Brand { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public int Id { get; set; }
        public int orgId { get; set; }
        public string[] ProductList { get; set; }
        public double OldPrice { get; set; }
        public int Quantity { get; set; }
        public int ProductQty { get; set; }
        public int UserId { get; set; }
        public int orderid { get; set; }
        public Guid orderGuId { get; set; }
        public string SpecificationNames { get; set; }
        public int SpecificationId { get; set; }

    }
}
