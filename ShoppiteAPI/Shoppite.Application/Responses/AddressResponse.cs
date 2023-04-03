using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Application.Responses
{
    public class AddressResponse
    {
        public int UserId { get; set; }
        public int orgId { get; set; }
        public string zipcode { get; set; }
        //  public string SelectCountry { get; set; }
        public string SelectCity { get; set; }
        public string SelectState { get; set; }
        public string AddressDetail { get; set; }
    }
}
