using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.DTOs
{
    public class DeviceListDTO
    {
        public int ID { get; set; }
        public string Token { get; set; }
        public bool IsIos { get; set; }
    }
}
