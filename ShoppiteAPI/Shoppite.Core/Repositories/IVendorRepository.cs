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

        Task<Vendor_Users_DTO> PostVendorUsers(Vendor_Users_DTO vendorUsers);

        Task<List<Vendor_Users_DTO>> GetAllVendorUsers(int org_id);
    }
}
