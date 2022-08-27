using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shoppite.Application.Commands;
using Shoppite.Core.DTOs;
using Shoppite.Core.Repositories;

namespace Shoppite.Application.Handlers.CommandHandlers
{
    class CreateVendorUserHandler: IRequestHandler<CreateVendorUsersCommand, Vendor_Users_DTO>
    {
        private readonly IVendorRepository _createVendorRepository;

        public CreateVendorUserHandler(IVendorRepository createVendorRepository)
        {
            _createVendorRepository = createVendorRepository;
        }
        public async Task<Vendor_Users_DTO> Handle(CreateVendorUsersCommand request, CancellationToken cancellationToken)
        {
            return await _createVendorRepository.PostVendorUsers(request.vendorusers);
        }
    }
}
