using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.Model
{
    public partial class Coupan
    {
        public int CoupanId { get; set; }
        public string CoupanCode { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
