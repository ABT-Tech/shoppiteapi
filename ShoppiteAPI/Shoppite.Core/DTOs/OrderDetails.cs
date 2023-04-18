using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.DTOs
{
    public class OrderDetails
    {
        public List<OrderListModel> ProductLists { get; set; }
        public string Date { get; set; }
        public string Address { get; set; }
        public object TotalPrice { get; set; }
        public int orgId { get; set; }
        public int userId { get; set; }       

    }
}
