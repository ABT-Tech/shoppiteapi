using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.DTOs
{
   public  class PaymentGatewayResponse
    {
        public string encryptedParams { get; set; }
        public string AggregatorRedirectionLink { get; set; }
        public string AggregatorCallbackURL { get; set; }
        public string merchantId { get; set; }
    }
}
