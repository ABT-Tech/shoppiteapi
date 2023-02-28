using System;
using System.Collections.Generic;

#nullable disable

namespace Shoppite.Core.Model
{
    public partial class Message
    {
        public int MesageId { get; set; }
        public Guid? ChatId { get; set; }
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public DateTime? Senddate { get; set; }
        public DateTime? Recieveddate { get; set; }
        public string Status { get; set; }
        public string Message1 { get; set; }
        public string Attachment { get; set; }
        public int? SessionBookingId { get; set; }
        public int? OrgId { get; set; }
    }
}
