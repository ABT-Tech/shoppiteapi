﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Shoppite.Core.Model
{
    public partial class UsersProfile
    {
        public int ProfileId { get; set; }
        public Guid ProfileGuid { get; set; }
        public string UserName { get; set; }
        public string Type { get; set; }
        public DateTime? InsertDate { get; set; }
        public string AdminStatus { get; set; }
        public string Status { get; set; }
        public string ShopName { get; set; }
        public string ShopUrlpath { get; set; }
        public string Logo { get; set; }
        public string CoverImage { get; set; }
        public string ContactNumber { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string State { get; set; }
        public string Address { get; set; }
        public string ShopDescription { get; set; }
        public string PaypalId { get; set; }
        public bool? IsMembershipFree { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int? OrgId { get; set; }
    }
}
