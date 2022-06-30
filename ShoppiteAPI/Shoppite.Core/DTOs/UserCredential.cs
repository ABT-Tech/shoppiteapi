using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.DTOs
{
    public class UserCredential
    {
        public int org_id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}
