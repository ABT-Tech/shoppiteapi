using System;
using System.Collections.Generic;
namespace Shoppite.Core.DTOs
{
   public class OrdersDTO
    {     
        public int? orgid { get; set; }
        public Guid? OrderGuid { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Contactnumber { get; set; }
        public List<ProductList> ProductLists { get; set; }
        public ChangeAddress Address { get; set; }
        public object BaseTotalPrice { get; set; }
        public bool OnePay { get; set; }
    }
}
