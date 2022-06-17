using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Application.Responses
{
    public class WishListResponse
    {
        public int id { get; set; }
        public int org_id { get; set; }
        public int sub_ctg_id { get; set; }
        public int category_id { get; set; }
        public int product_id { get; set; }
        public string wproduct_name { get; set; }
        public double wproduct_price { get; set; }
        public string product_image { get; set; }
        public bool is_available { get; set; }
    }
}
