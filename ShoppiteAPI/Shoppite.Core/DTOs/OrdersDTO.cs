using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.DTOs
{
   public class OrdersDTO
    {
        public int orgid { get; set; }
      /*  public List<ProductListModel> ProductLists { get; set; }
        public ChangeAddress Address { get; set; }*/
        public object BaseTotalPrice { get; set; }
    }
}
