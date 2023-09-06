using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.DTOs
{
    public class MasterProductDTO
    {
        public List<DetailProductDTO> MainProductDTOs { get; set; }
    }
    public class DetailProductDTO
    {
        public string Status { get; set; }
        public List<ProductsDTO> productsDTOs { get; set; }
    }
}
