using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.Model
{
    public class Notifications
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public Nullable<int> UserID { get; set; }
        public bool Sent { get; set; }
        public int NotificationType { get; set; }
        public Nullable<DateTime> SentDate { get; set; }
        public int OrgId { get; set; }
        public DateTime CreatedDate { get; set; }
        
    }
}
