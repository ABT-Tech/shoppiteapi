using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.DTOs
{
    public class Users_DTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string jwt_token { get; set; }
    }
}
