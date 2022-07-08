using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.DTOs
{
    public class Order_DTO
    {
        
        public int user_id { get; set; }
        public int org_id { get; set; }
        public int prod_quantity { get; set; }
        public int category_id { get; set; }
        public int sub_ctg_id { get; set; }
        public int product_id { get; set; }
        public string cproduct_name { get; set; }
        public float cproduct_price { get; set; }
        public string cproduct_image { get; set; }

    }
   
}
