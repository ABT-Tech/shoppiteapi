using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.DTOs
{
    public class Vendor_Users_DTO
    {
        public int user_id { get; set; }
        public int org_id { get; set; }
        public int role_id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string mobile_number { get; set; }
        public string email_id { get; set; }
        public string address_1 { get; set; }
        public string address_2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public int pincode { get; set; }
        public string id_proof { get; set; }
        public int isactive { get; set; }
        public DateTime created_date { get; set; }
        public DateTime updated_date { get; set; }

    }
}
