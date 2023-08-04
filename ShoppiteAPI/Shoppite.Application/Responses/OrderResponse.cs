using Shoppite.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Application.Responses
{
    public class OrderResponse
    {
        public int? orgid { get; set; }
        public Guid? OrderGuid { get; set; }
        public int OrderId { get; set; }
        public string UserName { get; set; }
        public string Contactnumber { get; set; }
        public List<ProductList> ProductLists { get; set; }
        public ChangeAddress Address { get; set; }
        public object BaseTotalPrice { get; set; }
        public bool OnePay { get; set; }
    }
}
