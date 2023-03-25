using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Application.Responses
{
    public class UserResponse
    {
        public int userId { get; set; }

        public string ChangeName { get; set; }
        public string ChangeEmail { get; set; }
        public string ChangePhoneNumber { get; set; }
       
        public string ChangeAddress { get; set; }
    }
}
