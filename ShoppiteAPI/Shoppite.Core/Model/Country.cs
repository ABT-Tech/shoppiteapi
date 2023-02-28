using System;
using System.Collections.Generic;

#nullable disable

namespace Shoppite.Core.Model
{
    public partial class Country
    {
        public int Countryid { get; set; }
        public string CountryName { get; set; }
        public string Image { get; set; }
        public string Status { get; set; }
        public DateTime? InsertDate { get; set; }
        public string InsertBy { get; set; }
    }
}
