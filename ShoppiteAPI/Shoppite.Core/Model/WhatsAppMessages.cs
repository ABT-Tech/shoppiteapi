using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.Model
{
    public partial class WhatsAppMessages
    {
        public int ID { get; set; }
        public string MobileNumber { get; set; }
        public string MessageRequest { get; set; }
        public string TemplateID { get; set; }
        public bool Is_SendMessage { get; set; }
        public string MessageResponse { get; set; }
        public string OrgName { get; set; }
        public DateTime? InsertDateTime { get; set; }
    }
}
