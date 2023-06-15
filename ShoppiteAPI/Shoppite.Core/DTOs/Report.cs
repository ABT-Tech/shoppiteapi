using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.DTOs
{
    public class Report
    {
        public string UserName { get; set; }
        public string Date { get; set; }
        public int TtlOrder { get; set; }
        public int orgId { get; set; }
        public int userId { get; set; }
    }
}
