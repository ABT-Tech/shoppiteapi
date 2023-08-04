﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.DTOs
{
    [Serializable]
    public class MerchantParams
    {
        public string merchantId { get; set; }

        public string apiKey { get; set; }

        public string txnId { get; set; } // Merchant Order Number

        public string amount { get; set; }

        public string dateTime { get; set; }

        public string custMail { get; set; }

        public string custMobile { get; set; }

        public string udf1 { get; set; }

        public string udf2 { get; set; }

        public string returnURL { get; set; }

        public string isMultiSettlement { get; set; }

        public string productId { get; set; }

        public string channelId { get; set; }

        public string txnType { get; set; }
        public string udf3 { get; set; } = "NA";
        public string udf4 { get; set; } = "NA";
        public string udf5 { get; set; } = "NA";
        public string instrumentId { get; set; } = "NA";
        public string cardDetails { get; set; } = "NA";
        public string cardType { get; set; } = "NA";
        public string ResellerTxnId { get; set; } = "NA";
        public string Rid { get; set; } = "R0000259";
    }
}
