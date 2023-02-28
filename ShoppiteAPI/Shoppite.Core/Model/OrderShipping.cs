using System;
using System.Collections.Generic;

#nullable disable

namespace Shoppite.Core.Model
{
    public partial class OrderShipping
    {
        public int ShippingId { get; set; }
        public Guid? OrderGuid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Phone { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Contactnumber { get; set; }
        public int? OrgId { get; set; }
    }
}
