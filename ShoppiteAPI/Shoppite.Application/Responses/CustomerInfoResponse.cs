using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Application.Responses
{
    public class CustomerInfoResponse
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string icon { get; set; }
        public int orgId { get; set; }
        public int userId { get; set; }
        public bool Active { get; set; }
    }
}
