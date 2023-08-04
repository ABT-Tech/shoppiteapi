using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.Model
{
   public  class Organization_Aggregator_Control
    {
        public int Id { get; set; }
        public string AggregatorMerchantId { get; set; }
        public string AggregatorMerchantApiKey { get; set; }
        public string AggregatorRID { get; set; }
        public string AggregatorCallbackURL { get; set; }
        public int OrgId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
