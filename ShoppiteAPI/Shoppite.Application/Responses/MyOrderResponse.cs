using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Application.Responses
{
    public class MyOrderResponse
    {
        public string Title { get; set; }
        public string Brand { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public int Id { get; set; }
        public int orgId { get; set; }
        public string[] ProductList { get; set; }
        public int OldPrice { get; set; }
        public int Quantity { get; set; }
        public int UserId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public Guid orderGuId { get; set; }
    }
}
