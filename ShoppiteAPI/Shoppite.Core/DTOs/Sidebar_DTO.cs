using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.DTOs
{
    public class Sidebar_DTO
    {
        public int id { get; set; }
        public int org_id { get; set; }
        public string sidemenu_name { get; set; }
        public string sidemenu_navlink { get; set; }
        public string icon_img { get; set; }
    }
}
