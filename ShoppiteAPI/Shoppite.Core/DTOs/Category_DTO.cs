using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.DTOs
{
    public class Category_DTO
    {
        public int id { get; set; }
        public int org_id { get; set; }
        public string category_code { get; set; }
        public string category_name { get; set; }
        public string category_description { get; set; }
        public string category_image { get; set; }
    }
}
