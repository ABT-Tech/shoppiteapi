using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.DTOs
{
    public class CoupanDTO
    {
        public int CoupanId { get; set; }
        public int UserId { get; set; }
        public string CoupanCode { get; set; }
        public int OrgId { get; set; }

    }
}
