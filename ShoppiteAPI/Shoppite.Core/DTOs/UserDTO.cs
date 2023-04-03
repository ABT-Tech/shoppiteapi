using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.DTOs
{
    public class UserDTO
    {
        public int userId { get; set; }
        public int OrgId { get; set; }
        public string ChangeName { get; set; }
        public string ChangeEmail { get; set; }
        public string ChangePhoneNumber { get; set; }     
        public string ChangeCity { get; set; }
        public string ChangeState { get; set; }
        public string ChangeAddress { get; set; }
        public string Zipcode { get; set; }
    }
}
