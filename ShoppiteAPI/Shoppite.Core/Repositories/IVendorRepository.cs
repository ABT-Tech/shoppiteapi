using Shoppite.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.Repositories
{
    public interface IVendorRepository
    {
        Task<Vendor_DTO> PostVendor(Vendor_DTO vendor);
    }
}
