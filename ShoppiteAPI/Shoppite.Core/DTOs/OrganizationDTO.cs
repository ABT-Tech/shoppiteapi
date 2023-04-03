using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.DTOs
{
    public class OrganizationDTO
    {
        public string ShopName { get; set; }
        public int OrgId { get; set; }
        public string Image { get; set; }
        public string VenderName { get; set; }
        public string VenderEmail { get; set; }
        public string VenderMobile { get; set; }
        public string OrgDescription { get; set; }
    }
}
