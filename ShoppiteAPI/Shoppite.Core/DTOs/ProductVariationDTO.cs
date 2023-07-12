using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.DTOs
{
    public class ProductVariationDTO
    {
        public string SpecificationNames { get; set; }
        public int SpecificationIds { get; set; }
        public bool IsSpecificationExist { get; set; }
        public Guid ProductGUId { get; set; }
        public int OrgId { get; set; }
    }
}
