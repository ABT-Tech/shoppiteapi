using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.DTOs
{
    public class UserCoupanResponse
    {
        public int CoupanId { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
