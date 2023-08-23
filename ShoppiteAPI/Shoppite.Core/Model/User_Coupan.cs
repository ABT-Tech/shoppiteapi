using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.Model
{
    public partial class User_Coupan
    {
        public int CoupanUserId { get; set; }
        public int CoupanId { get; set; }
        public int UserId { get; set; }
        public int OrgId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ContactNumber { get; set; }
    }
}
