using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shoppite.Application.Queries;
using Shoppite.Core.Repositories;
using System.Threading;

namespace Shoppite.Application.Handlers.QueryHandlers
{
    public class GetAllVendorUsersHandler : IRequestHandler<GetAllVendorUsersQuery, List<Core.DTOs.Vendor_Users_DTO>>
    {
       private readonly IVendorRepository _vendorrepository;

       public GetAllVendorUsersHandler(IVendorRepository vendorRepository)
        {
            _vendorrepository = vendorRepository;
        }
        public async Task<List<Core.DTOs.Vendor_Users_DTO>> Handle(GetAllVendorUsersQuery request, CancellationToken cancellationToken)
        {
            return (List<Core.DTOs.Vendor_Users_DTO>)await _vendorrepository.GetAllVendorUsers(request.org_id);
        }
    }
}
