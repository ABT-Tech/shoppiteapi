using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.DTOs
{
    public class Users_DTO
    {
        public int id { get; set; }
        public string type { get; set; }
        public int org_id { get; set; }
        public string user_name { get; set; }
        public string f_name { get; set; }
        public string l_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phone_number { get; set; }
        public string address_1 { get; set; }
        public string address_2 { get; set; }
        public string area { get; set; }
        public int city { get; set; }
        public int state { get; set; }
        public int country { get; set; }
        public int pincode { get; set; }
        public bool? is_active { get; set; }
        public string jwt_token { get; set; }
    }
}
