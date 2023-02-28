using System;
using System.Collections.Generic;

#nullable disable

namespace Shoppite.Core.Model
{
    public partial class Currency
    {
        public int CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public bool? IsPublished { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UserName { get; set; }
        public bool? Isbase { get; set; }
    }
}
