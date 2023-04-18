using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.Model
{
    public partial class Notifications_Token
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string MacID { get; set; }
        public string Token { get; set; }
        public DateTime InsertedAt { get; set; }
    }
}
