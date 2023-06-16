using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Application.Responses
{
    public class NumberOfCartItemResponse
    {
        public int NumOfItems { get; set; }
        public int UserId { get; set; }
        public int OrgId { get; set; }
    }
}
