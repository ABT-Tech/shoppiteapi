using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.DTOs
{
    public class NotificationsDataDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int OrgId { get; set; }
        public int UserID { get; set; }
        public string org_name { get; set; }
        public string Details { get; set; }
        public string logo { get; set; }
    }
}
