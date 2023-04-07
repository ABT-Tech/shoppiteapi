using Shoppite.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Application.Responses
{
    public class OrderDetailResponse
    {
        public List<ProductsDTO> ProductLists { get; set; }
        public DateTime Date { get; set; }
        public string Address { get; set; }
        public object TotalPrice { get; set; }
        public int orgId { get; set; }
        public int userId { get; set; }
    }
}
