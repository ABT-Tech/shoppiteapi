using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.DTOs
{
    public class CustomerInfo
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string icon { get; set; }
        public int orgId { get; set; }
        public int userId { get; set; }
        public string Status { get; set; }
        public bool Active { get; set; }
    }
}
