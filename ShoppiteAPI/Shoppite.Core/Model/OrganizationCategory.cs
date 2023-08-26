using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.Model
{
    public class OrganizationCategory
    {
        public int Org_CategoryId { get; set; }
        public int CategoryName { get; set; }
        public string CategoryImage { get; set; }
        public Boolean IsActive { get; set; }
        public int SortOrder { get; set; }

    }
}
