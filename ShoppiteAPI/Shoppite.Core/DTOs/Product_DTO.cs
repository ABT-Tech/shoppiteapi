using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.DTOs
{
    public class Product_DTO
    {
        public int id { get; set; }
        public int org_id { get; set; }
        public int sub_ctg_id { get; set; }
        public int category_id { get; set; }
        public string company_name { get; set; }
        public string product_name { get; set; }
        public string product_description { get; set; }
        public string product_code { get; set; }
        public double product_price { get; set; }
        public double product_discount { get; set; }
        public int product_quantity { get; set; }
        public string product_image { get; set; }
        public bool is_available { get; set; }
    }
}
