using Shoppite.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Application.Responses
{
    public class ProductVariationResponse
    {
        public string SpecificationNames { get; set; }
        public int SpecificationId { get; set; }
        public bool IsSpecificationExist { get; set; }
        public Guid ProductGUId { get; set; }
        public int OrgId { get; set; }
        public int DefaultSpecification { get; set; }
    }
}
