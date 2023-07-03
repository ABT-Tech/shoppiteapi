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
        public int Id { get; set; }
        public int OrgId { get; set; }
        public List<ProductVariationDetails> VariationDetails { get; set; }
    }
}
