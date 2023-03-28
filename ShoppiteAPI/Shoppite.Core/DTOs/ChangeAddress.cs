using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.DTOs
{
    public class ChangeAddress
    {
        //public int AddressId { get; set; }
        public int UserId { get; set; }
        public int orgId { get; set; }
        public string AddressTitle { get; set; }       
      //  public string SelectCountry { get; set; }
        public string zipcode { get; set; }
        public string SelectCity { get; set; }
        public string SelectState { get; set; }
        public string AddressDetail { get; set; }
    }
}
