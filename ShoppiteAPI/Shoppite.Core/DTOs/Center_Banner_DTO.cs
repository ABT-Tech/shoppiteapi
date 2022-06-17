using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.DTOs
{
    public class Center_Banner_DTO
    {
        public int id { get; set; }
        public int org_id { get; set; }
        public int category_id { get; set; }
        public int sub_category_id { get; set; }
        public int product_id { get; set; }
        public int banner_type_id { get; set; }
        public string banner_img { get; set; }
    }
}
