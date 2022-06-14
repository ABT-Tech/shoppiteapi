using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.DTOs
{
    public class SubCategory_By_Category_Group
    {
		public int category_id { get; set; }
		public string category_name { get; set; }
		public string category_image { get; set; }
		public List<SubCategory_DTO> subCategory_DTOs { get; set; }
	}
	public class SubCategory_DTO
    {
		public int sub_category_id { get; set; }
		public string sub_ctg_name { get; set; }
		public string sub_ctg_description { get; set; }
		public string sub_ctg_code { get; set; }
		public string sub_ctg_image { get; set; }
		public int org_id { get; set; }
		public string sub_ctg_nav { get; set; }
	}
}
